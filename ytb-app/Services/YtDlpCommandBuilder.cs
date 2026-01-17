using ytb_app.Models;
using System.Text;

namespace ytb_app.Services
{
    public class YtDlpCommandBuilder
    {
        public string BuildArguments(DownloadOptions options)
        {
            var sb = new StringBuilder();

            // Basic options
            sb.Append("-f \"bestaudio[ext=m4a]/bestaudio\" ");
            sb.Append("--no-overwrites ");
            sb.Append("--embed-thumbnail ");
            sb.Append("--add-metadata ");

            // Metadata: Album and Artist
            sb.Append($"--parse-metadata \"album:{options.Album}\" ");
            sb.Append($"--parse-metadata \"artist:{options.Artist}\" ");

            // Title replacement regex logic
            // BAT: --replace-in-metadata title ".*Chương\s*([0-9]+\s*-\s*[0-9]+).*" "\1"
            // We need to escape double quotes and backslashes if necessary.
            // In C# string, the backslashes in regex need to be preserved for yt-dlp to see them.
            string regex = @".*Chương\s*([0-9]+\s*-\s*[0-9]+).*";
            string replacement = @"\1";
            sb.Append($"--replace-in-metadata title \"{regex}\" \"{replacement}\" ");

            // Output template
            // Note: We'll set the working directory for the process, so -o can be just the filename template
            sb.Append("-o \"%(title)s.%(ext)s\" ");

            // Progress reporting
            sb.Append("--progress --newline ");

            // Finally, the URL
            sb.Append($"\"{options.PlaylistUrl}\"");

            return sb.ToString();
        }
    }
}
