using Microsoft.AspNetCore.Identity;

namespace Spotify.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Musician> FollowedMusicians { get; set; }
        public ICollection<Album> LikedAlbums { get; set; }
        public ICollection<Song> LikedSongs { get; set; }
    }
}
