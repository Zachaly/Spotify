
namespace Spotify.Domain.Models
{
    public class MusicianFollow
    {
        public int MusicianId { get; set; }
        public Musician Musician { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
