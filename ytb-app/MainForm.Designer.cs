namespace ytb_app
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tblRoot = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblUrlRow = new System.Windows.Forms.TableLayoutPanel();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnLoadInfo = new System.Windows.Forms.Button();
            this.tblFolderRow = new System.Windows.Forms.TableLayoutPanel();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tblActionsRow = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.dgvPlaylist = new System.Windows.Forms.DataGridView();
            this.pnlLogArea = new System.Windows.Forms.Panel();
            this.tblLog = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tblRoot.SuspendLayout();
            this.tblUrlRow.SuspendLayout();
            this.tblFolderRow.SuspendLayout();
            this.tblActionsRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).BeginInit();
            this.pnlLogArea.SuspendLayout();
            this.tblLog.SuspendLayout();
            this.SuspendLayout();

            // tblRoot
            this.tblRoot.ColumnCount = 1;
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.Controls.Add(this.lblTitle, 0, 0);
            this.tblRoot.Controls.Add(this.tblUrlRow, 0, 1);
            this.tblRoot.Controls.Add(this.tblFolderRow, 0, 2);
            this.tblRoot.Controls.Add(this.tblActionsRow, 0, 3);
            this.tblRoot.Controls.Add(this.dgvPlaylist, 0, 4);
            this.tblRoot.Controls.Add(this.pnlLogArea, 0, 5);
            this.tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoot.Location = new System.Drawing.Point(0, 0);
            this.tblRoot.Name = "tblRoot";
            this.tblRoot.Padding = new System.Windows.Forms.Padding(12);
            this.tblRoot.RowCount = 6;
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblRoot.Size = new System.Drawing.Size(900, 650);

            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "YouTube Audio Downloader";

            // tblUrlRow
            this.tblUrlRow.ColumnCount = 3;
            this.tblUrlRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblUrlRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblUrlRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblUrlRow.Controls.Add(this.lblUrl, 0, 0);
            this.tblUrlRow.Controls.Add(this.txtUrl, 1, 0);
            this.tblUrlRow.Controls.Add(this.btnLoadInfo, 2, 0);
            this.tblUrlRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUrl.Text = "Playlist URL:";
            this.lblUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUrl.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLoadInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadInfo.Text = "Load Info";
            this.btnLoadInfo.Click += new System.EventHandler(this.btnLoadInfo_Click);

            // tblFolderRow
            this.tblFolderRow.ColumnCount = 3;
            this.tblFolderRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblFolderRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFolderRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblFolderRow.Controls.Add(this.lblOutputDir, 0, 0);
            this.tblFolderRow.Controls.Add(this.txtOutputDir, 1, 0);
            this.tblFolderRow.Controls.Add(this.btnBrowse, 2, 0);
            this.tblFolderRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutputDir.Text = "Download To:";
            this.lblOutputDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOutputDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputDir.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            // tblActionsRow
            this.tblActionsRow.ColumnCount = 2;
            this.tblActionsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblActionsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblActionsRow.Controls.Add(this.btnDownload, 0, 0);
            this.tblActionsRow.Controls.Add(this.btnPause, 1, 0);
            this.tblActionsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Text = "START / RESUME DOWNLOAD";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPause.Text = "PAUSE";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);

            // dgvPlaylist
            this.dgvPlaylist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlaylist.AllowUserToAddRows = false;
            this.dgvPlaylist.AllowUserToDeleteRows = false;
            this.dgvPlaylist.AllowUserToResizeColumns = false;
            this.dgvPlaylist.AllowUserToResizeRows = false;
            this.dgvPlaylist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPlaylist.Location = new System.Drawing.Point(15, 175);
            this.dgvPlaylist.Name = "dgvPlaylist";
            this.dgvPlaylist.RowHeadersVisible = false;

            // pnlLogArea
            this.pnlLogArea.Controls.Add(this.tblLog);
            this.pnlLogArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLog.ColumnCount = 1;
            this.tblLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLog.Controls.Add(this.progressBar, 0, 0);
            this.tblLog.Controls.Add(this.txtLog, 0, 1);
            this.tblLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLog.RowCount = 2;
            this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.tblRoot);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Audio Downloader v1.0.0 | Author: khuongnv@live.com";
            
            this.tblRoot.ResumeLayout(false);
            this.tblUrlRow.ResumeLayout(false);
            this.tblUrlRow.PerformLayout();
            this.tblFolderRow.ResumeLayout(false);
            this.tblFolderRow.PerformLayout();
            this.tblActionsRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).EndInit();
            this.pnlLogArea.ResumeLayout(false);
            this.tblLog.ResumeLayout(false);
            this.tblLog.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tblRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblUrlRow;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnLoadInfo;
        private System.Windows.Forms.TableLayoutPanel tblFolderRow;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TableLayoutPanel tblActionsRow;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.DataGridView dgvPlaylist;
        private System.Windows.Forms.Panel pnlLogArea;
        private System.Windows.Forms.TableLayoutPanel tblLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox txtLog;
    }
}
