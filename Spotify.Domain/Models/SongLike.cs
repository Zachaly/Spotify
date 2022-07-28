
namespace Spotify.Domain.Models
{
    public class SongLike
    {
        public int SongId { get; set; }
        public Song Song { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
