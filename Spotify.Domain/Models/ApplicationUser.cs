using Microsoft.AspNetCore.Identity;

namespace Spotify.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<MusicianFollow> FollowedMusicians { get; set; }
        public ICollection<AlbumLike> LikedAlbums { get; set; }
        public ICollection<SongLike> LikedSongs { get; set; }
    }
}
