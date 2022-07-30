
namespace Spotify.UI.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public long Plays { get; set; } = 0;
    }
}
