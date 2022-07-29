using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IApplicationUserManager
    {
        public ApplicationUser GetUserByEmail(string email);
        public bool IsEmailOccupied(string email);
        public T GetUserById<T>(string id, Func<ApplicationUser, T> selector);
        public IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector);

        public Task<bool> FollowMusician(string userId, int musicianId);
        public Task<bool> LikeSong(string userId, int songId);
        public Task<bool> LikeAlbum(string userId, int musicianId);

        public bool IsSongLiked(string userId, int songId);
        public bool IsMusicianFollowed(string userId, int musicianId);
        public bool IsAlbumLiked(string userId, int albumId);
    }
}
