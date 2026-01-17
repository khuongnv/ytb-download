namespace ytb_app.Models
{
    public class PlaylistInfo
    {
        public string Title { get; set; } = string.Empty;
        public int? VideoCount { get; set; }
        public string Uploader { get; set; } = string.Empty;
        public List<PlaylistItem> Entries { get; set; } = new();
    }
}
