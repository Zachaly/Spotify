
namespace Spotify.UI.ViewModels
{
    public class MusicianViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Plays { get; set; } = 0;
        public string FileName { get; set; }
    }
}
