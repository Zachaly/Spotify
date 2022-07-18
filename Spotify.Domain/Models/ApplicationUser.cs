using Microsoft.AspNetCore.Identity;

namespace Spotify.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Artist> FollowedArtists { get; set; }
        public ICollection<Album> LikedAlbums { get; set; }
        public ICollection<Song> LikedSongs { get; set; }
    }
}
