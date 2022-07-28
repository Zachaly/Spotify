using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IApplicationUserManager
    {
        public ApplicationUser GetUserByEmail(string email);
        public bool IsEmailOccupied(string email);
        public T GetUserById<T>(string id, Func<ApplicationUser, T> selector);
        public IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector);
    }
}
