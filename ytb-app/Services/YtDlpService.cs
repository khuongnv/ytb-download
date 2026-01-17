using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using ytb_app.Models;

namespace ytb_app.Services
{
    public class YtDlpService
    {
        public event Action<string>? OnLog;
        public event Action<int>? OnProgress;
        public event Action<bool>? OnFinished;

        private readonly Regex _progressRegex = new(@"\[download\]\s+(\d+\.?\d*)%", RegexOptions.Compiled);
        private readonly string _ytDlpPath;

        public YtDlpService(string ytDlpPath)
        {
            _ytDlpPath = ytDlpPath;
        }

        public async Task<bool> DownloadAsync(DownloadOptions options, string arguments, CancellationToken ct = default)
        {
            if (!File.Exists(_ytDlpPath))
            {
                OnLog?.Invoke($"Error: yt-dlp.exe not found at {_ytDlpPath}");
                return false;
            }

            if (!Directory.Exists(options.OutputDir))
            {
                Directory.CreateDirectory(options.OutputDir);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = _ytDlpPath,
                Arguments = arguments,
                WorkingDirectory = options.OutputDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            using var process = new Process { StartInfo = startInfo };

            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    OnLog?.Invoke(e.Data);
                    ParseProgress(e.Data);
                }
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    OnLog?.Invoke($"ERROR: {e.Data}");
                }
            };

            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                try
                {
                    await process.WaitForExitAsync(ct);
                }
                catch (OperationCanceledException)
                {
                    if (!process.HasExited)
                    {
                        process.Kill(true);
                        OnLog?.Invoke("Download cancelled.");
                    }
                    return false;
                }

                bool success = process.ExitCode == 0;
                OnFinished?.Invoke(success);
                return success;
            }
            catch (Exception ex)
            {
                OnLog?.Invoke($"Exception: {ex.Message}");
                OnFinished?.Invoke(false);
                return false;
            }
        }

        public async Task<PlaylistInfo?> GetPlaylistInfoAsync(string url)
        {
            if (!File.Exists(_ytDlpPath))
            {
                OnLog?.Invoke($"Error: yt-dlp.exe not found at {_ytDlpPath}");
                return null;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = _ytDlpPath,
                Arguments = $"--flat-playlist --dump-single-json \"{url}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            using var process = new Process { StartInfo = startInfo };
            string output = string.Empty;
            string error = string.Empty;

            try
            {
                process.Start();
                output = await process.StandardOutput.ReadToEndAsync();
                error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    OnLog?.Invoke($"Error fetching playlist info: {error}");
                    return null;
                }

                using var jsonDoc = JsonDocument.Parse(output);
                var root = jsonDoc.RootElement;

                var playlistTitle = root.TryGetProperty("title", out var titleProp) ? titleProp.GetString() ?? "Unknown" : "Unknown";
                var uploader = root.TryGetProperty("uploader", out var uploaderProp) ? uploaderProp.GetString() ?? "Unknown" : "Unknown";

                var info = new PlaylistInfo
                {
                    Title = playlistTitle,
                    Uploader = uploader
                };

                if (root.TryGetProperty("entries", out var entriesProp) && entriesProp.ValueKind == JsonValueKind.Array)
                {
                    int index = 1;
                    foreach (var entry in entriesProp.EnumerateArray())
                    {
                        string originalTitle = entry.TryGetProperty("title", out var t) ? t.GetString() ?? "" : "";
                        string entryUrl = entry.TryGetProperty("url", out var u) ? u.GetString() ?? "" : "";
                        
                        // Apply regex processing if uploader/artist/album logic is similar
                        // For now, let's process the title as per requirements
                        string processedTitle = ProcessTitle(originalTitle);

                        info.Entries.Add(new PlaylistItem
                        {
                            Id = index++,
                            OriginalTitle = originalTitle,
                            Title = processedTitle,
                            Url = entryUrl,
                            Album = playlistTitle, // Default to playlist title
                            Artist = uploader      // Default to playlist uploader
                        });
                    }
                    info.VideoCount = info.Entries.Count;
                }

                return info;
            }
            catch (Exception ex)
            {
                OnLog?.Invoke($"Exception fetching playlist info: {ex.Message}");
                return null;
            }
        }

        private string ProcessTitle(string title)
        {
            try
            {
                // Regex: .*Chương\s*([0-9]+\s*-\s*[0-9]+).* -> \1
                var regex = new Regex(@".*Chương\s*([0-9]+\s*-\s*[0-9]+).*", RegexOptions.IgnoreCase);
                var match = regex.Match(title);
                if (match.Success)
                {
                    return match.Groups[1].Value.Trim();
                }
            }
            catch { }
            return title;
        }

        private void ParseProgress(string output)
        {
            var match = _progressRegex.Match(output);
            if (match.Success)
            {
                if (double.TryParse(match.Groups[1].Value, out double percentage))
                {
                    OnProgress?.Invoke((int)Math.Round(percentage));
                }
            }
        }
    }
}
