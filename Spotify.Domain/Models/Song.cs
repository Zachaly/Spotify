
namespace Spotify.Domain.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MusicianId { get; set; }
        public Musician Creator { get; set; }
        public long Plays { get; set; } = 0;
    }
}
