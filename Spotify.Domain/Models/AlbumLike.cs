
namespace Spotify.Domain.Models
{
    public class AlbumLike
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
