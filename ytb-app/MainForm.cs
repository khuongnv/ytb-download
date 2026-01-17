using System.ComponentModel;
using System.Text.Json;
using ytb_app.Models;
using ytb_app.Services;

namespace ytb_app
{
    public partial class MainForm : Form
    {
        private readonly YtDlpService _service;
        private readonly YtDlpCommandBuilder _commandBuilder;
        private BindingList<PlaylistItem> _playlistEntries = new();
        private PlaylistItem? _currentlyDownloadingItem;
        
        private CancellationTokenSource? _cts;
        private readonly string _stateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appstate.json");

        public MainForm()
        {
            // Resolve yt-dlp path
            string ytDlpPath = Path.Combine("d:\\SRC\\ytb-download\\yt-bin", "yt-dlp.exe");
            
            _service = new YtDlpService(ytDlpPath);
            _commandBuilder = new YtDlpCommandBuilder();

            InitializeComponent();
            SetupDataGridView();
            UIState(true);

            // Set Application Icon
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YT_Audio_Downloader.ico");
            if (File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }
            else
            {
                // Fallback to searching in project directory during debug
                string debugIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\YT_Audio_Downloader.ico");
                if (File.Exists(debugIconPath)) this.Icon = new Icon(debugIconPath);
            }

            _service.OnLog += Log;
            _service.OnProgress += UpdateProgress;

            // Default output directory
            txtOutputDir.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "YouTube Downloads");
            
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void SetupDataGridView()
        {
            dgvPlaylist.AutoGenerateColumns = false;
            dgvPlaylist.DataSource = _playlistEntries;

            dgvPlaylist.AllowUserToResizeColumns = false;
            dgvPlaylist.AllowUserToResizeRows = false;
            dgvPlaylist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvPlaylist.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsSelected", HeaderText = "", Width = 30, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "STT", Width = 40, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Tiêu đề", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album", Width = 150, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist", Width = 120, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100, Resizable = DataGridViewTriState.False });
            dgvPlaylist.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Progress", HeaderText = "%", Width = 50, Resizable = DataGridViewTriState.False });

            dgvPlaylist.CurrentCellDirtyStateChanged += DgvPlaylist_CurrentCellDirtyStateChanged;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            LoadState();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveState();
        }

        private void SaveState()
        {
            try
            {
                var state = new AppState
                {
                    PlaylistUrl = txtUrl.Text,
                    OutputDir = txtOutputDir.Text,
                    PlaylistEntries = _playlistEntries.ToList()
                };
                string json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_stateFilePath, json);
            }
            catch (Exception ex)
            {
                Log($"Failed to save state: {ex.Message}");
            }
        }

        private void LoadState()
        {
            if (!File.Exists(_stateFilePath)) return;

            try
            {
                string json = File.ReadAllText(_stateFilePath);
                var state = JsonSerializer.Deserialize<AppState>(json);
                if (state != null)
                {
                    txtUrl.Text = state.PlaylistUrl;
                    txtOutputDir.Text = state.OutputDir;
                    
                    _playlistEntries.Clear();
                    foreach (var item in state.PlaylistEntries)
                    {
                        _playlistEntries.Add(item);
                    }
                    dgvPlaylist.Refresh();
                    Log("Previous state restored.");
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to load state: {ex.Message}");
            }
        }

        private async void btnLoadInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("Please enter a Playlist URL.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UIState(false);
            Log("Fetching playlist info...");
            _playlistEntries.Clear();

            var info = await _service.GetPlaylistInfoAsync(txtUrl.Text.Trim());

            if (info != null)
            {
                foreach (var entry in info.Entries)
                {
                    string filePath = Path.Combine(txtOutputDir.Text.Trim(), entry.Title + ".m4a");
                    if (File.Exists(filePath))
                    {
                        entry.IsSelected = false;
                        entry.Status = "Exists";
                    }
                    _playlistEntries.Add(entry);
                }

                Log($"Playlist loaded: {info.Title} ({info.Entries.Count} items)");
            }
            else
            {
                Log("Failed to fetch playlist info.");
                MessageBox.Show("Failed to fetch playlist info. Check the log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UIState(true);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputDir.Text = dialog.SelectedPath;
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (_playlistEntries.Count == 0 || !_playlistEntries.Any(x => x.IsSelected))
            {
                MessageBox.Show("No items selected to download.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _cts = new CancellationTokenSource();
            UIState(false);

            int total = _playlistEntries.Count;
            int count = _playlistEntries.Count(x => x.Status == "Done");
            progressBar.Value = total > 0 ? (int)((double)count / total * 100) : 0;

            try
            {
                foreach (var item in _playlistEntries)
                {
                    if (!item.IsSelected || item.Status == "Done") continue;

                    _currentlyDownloadingItem = item;
                    item.Status = "Downloading";
                    dgvPlaylist.Refresh();

                    var options = new DownloadOptions
                    {
                        PlaylistUrl = item.Url,
                        Album = item.Album,
                        Artist = item.Artist,
                        OutputDir = txtOutputDir.Text.Trim()
                    };

                    string arguments = _commandBuilder.BuildArguments(options);
                    Log($"Downloading {item.Id}/{total}: {item.Title}");

                    bool success = await _service.DownloadAsync(options, arguments, _cts.Token);

                    if (_cts.IsCancellationRequested)
                    {
                        item.Status = "Paused";
                        dgvPlaylist.Refresh();
                        break;
                    }

                    item.Status = success ? "Done" : "Error";
                    item.Progress = success ? 100 : 0;
                    dgvPlaylist.Refresh();

                    if (success) count++;
                    progressBar.Value = (int)((double)count / total * 100);
                }
            }
            catch (Exception ex)
            {
                Log($"Error in download loop: {ex.Message}");
            }
            finally
            {
                _currentlyDownloadingItem = null;
                UIState(true);
                
                if (_cts != null && _cts.IsCancellationRequested)
                {
                    Log("Download queue paused.");
                }
                else
                {
                    Log("All available downloads processed.");
                }
                
                _cts?.Dispose();
                _cts = null;
                
                SaveState();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Log("Pausing...");
            _cts?.Cancel();
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(Log), message);
                return;
            }

            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        }

        private void UpdateProgress(int percentage)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(UpdateProgress), percentage);
                return;
            }

            if (_currentlyDownloadingItem != null)
            {
                _currentlyDownloadingItem.Progress = percentage;
                dgvPlaylist.Refresh();
            }
        }

        private void UIState(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UIState(enabled)));
                return;
            }

            txtUrl.Enabled = enabled;
            txtOutputDir.Enabled = enabled;
            btnBrowse.Enabled = enabled;
            btnLoadInfo.Enabled = enabled;
            btnDownload.Enabled = enabled;
            btnPause.Enabled = !enabled;
        }

        private void DgvPlaylist_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dgvPlaylist.IsCurrentCellDirty)
            {
                dgvPlaylist.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
