using System;
using System.IO;

namespace ytb_app.Services
{
    public class StorageService
    {
        private readonly string _basePath;

        public StorageService()
        {
            // Use %LOCALAPPDATA%\YTDownloader
            _basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "YTDownloader"
            );

            EnsureDirectories();
        }

        public string GetStateFilePath()
        {
            return Path.Combine(_basePath, "appstate.json");
        }

        public string GetLogsDirectory()
        {
            return Path.Combine(_basePath, "logs");
        }

        public string GetConfigFilePath()
        {
            return Path.Combine(_basePath, "config.json");
        }

        private void EnsureDirectories()
        {
            try
            {
                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                }

                string logsDir = GetLogsDirectory();
                if (!Directory.Exists(logsDir))
                {
                    Directory.CreateDirectory(logsDir);
                }
            }
            catch (Exception)
            {
                // In a real app we might want to fallback or log this, 
                // but for now we expect LocalAppData to be writable.
            }
        }
    }
}
