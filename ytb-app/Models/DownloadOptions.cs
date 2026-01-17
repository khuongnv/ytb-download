namespace ytb_app.Models
{
    public class DownloadOptions
    {
        public string PlaylistUrl { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string OutputDir { get; set; } = string.Empty;
    }
}
