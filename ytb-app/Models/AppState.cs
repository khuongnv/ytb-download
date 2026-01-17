using System.Collections.Generic;

namespace ytb_app.Models
{
    public class AppState
    {
        public string PlaylistUrl { get; set; } = string.Empty;
        public string OutputDir { get; set; } = string.Empty;
        public List<PlaylistItem> PlaylistEntries { get; set; } = new();
    }
}
