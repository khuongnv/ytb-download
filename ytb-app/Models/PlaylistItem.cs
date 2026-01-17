namespace ytb_app.Models
{
    public class PlaylistItem
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; } = true;
        public string Title { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public int Progress { get; set; } = 0;
    }
}
