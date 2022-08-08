using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IApplicationUserManager
    {
        ApplicationUser GetUserByEmail(string email);
        bool IsEmailOccupied(string email);
        T GetUserById<T>(string id, Func<ApplicationUser, T> selector);
        IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector);

        Task<bool> FollowMusician(string userId, int musicianId);
        Task<bool> LikeSong(string userId, int songId);
        Task<bool> LikeAlbum(string userId, int musicianId);

        bool IsSongLiked(string userId, int songId);
        bool IsMusicianFollowed(string userId, int musicianId);
        bool IsAlbumLiked(string userId, int albumId);

        Task<bool> UpdateUser(string id, Action<ApplicationUser> updatedValues);
        Task<bool> SetDefaultProfilePicture(string id, string picture);

        bool IsUserManagerOfMusician(string userId, int musicianId);
    }
}
