
namespace Spotify.Domain.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public Artist Creator { get; set; }
        public long Plays { get; set; } = 0;
    }
}
