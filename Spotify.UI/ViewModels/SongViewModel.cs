
namespace Spotify.UI.ViewModels
{
    public class SongViewModel
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public long Plays { get; set; }
        public string CreatorName { get; set; }
        public int CreatorId { get; set; }
    }
}
