using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private AppDbContext _dbContext;

        public ApplicationUserManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ApplicationUser GetUserByEmail(string email) 
            => _dbContext.Users.FirstOrDefault(user => user.Email == email);

        public bool IsEmailOccupied(string email)
            => _dbContext.Users.AsEnumerable().Any(user => user.Email == email);

        public T GetUserById<T>(string id, Func<ApplicationUser, T> selector)
            => _dbContext.Users.Include(db => db.FollowedMusicians).ThenInclude(db => db.Musician).
                Include(db => db.LikedAlbums).ThenInclude(db => db.Album).
                Where(user => user.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector)
            => _dbContext.Users.Include(db => db.LikedSongs).ThenInclude(db => db.Song).ThenInclude(db => db.Album).
                Include(db => db.LikedSongs).ThenInclude(db => db.Song).ThenInclude(db => db.Creator).
                FirstOrDefault(user => user.Id == userId)?.
                LikedSongs.Select(selector).
                AsEnumerable();
    }
}
