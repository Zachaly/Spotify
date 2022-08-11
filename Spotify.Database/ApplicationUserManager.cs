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
                Include(db => db.LikedSongs).
                Where(user => user.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector)
            => _dbContext.Users.Include(db => db.LikedSongs).ThenInclude(db => db.Song).ThenInclude(db => db.Album).
                Include(db => db.LikedSongs).ThenInclude(db => db.Song).ThenInclude(db => db.Creator).
                FirstOrDefault(user => user.Id == userId)?.
                LikedSongs.Select(selector).
                AsEnumerable();

        public async Task<bool> FollowMusician(string userId, int musicianId)
        {
            var follow = new MusicianFollow
            {
                ApplicationUserId = userId,
                MusicianId = musicianId,
            };

            var userFollows = _dbContext.MusicianFollows.Where(x => x.ApplicationUserId == userId);

            if(userFollows.Any(x => x.MusicianId == musicianId))
                _dbContext.MusicianFollows.Remove(userFollows.FirstOrDefault(x => x.MusicianId == musicianId));
            else
                _dbContext.MusicianFollows.Add(follow);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> LikeSong(string userId, int songId)
        {
            var like = new SongLike
            {
                ApplicationUserId = userId,
                SongId = songId,
            };

            var userLikes = _dbContext.SongLikes.Where(x => x.ApplicationUserId == userId);

            if (userLikes.Any(x => x.SongId == songId))
                _dbContext.SongLikes.Remove(userLikes.FirstOrDefault(x => x.SongId == songId));
            else
                _dbContext.SongLikes.Add(like);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> LikeAlbum(string userId, int albumId)
        {
            var like = new AlbumLike
            {
                ApplicationUserId = userId,
                AlbumId = albumId,
            };

            var userLikes = _dbContext.AlbumLikes.Where(x => x.ApplicationUserId == userId);

            if (userLikes.Any(x => x.AlbumId == albumId))
                _dbContext.AlbumLikes.Remove(userLikes.FirstOrDefault(x => x.AlbumId == albumId));
            else
                _dbContext.AlbumLikes.Add(like);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool IsSongLiked(string userId, int songId)
            => _dbContext.SongLikes.Any(x => x.SongId == songId && x.ApplicationUserId == userId);

        public bool IsMusicianFollowed(string userId, int musicianId)
            => _dbContext.MusicianFollows.Any(x => x.MusicianId == musicianId && x.ApplicationUserId == userId);

        public bool IsAlbumLiked(string userId, int albumId)
            => _dbContext.AlbumLikes.Any(x => x.AlbumId == albumId && x.ApplicationUserId == userId);

        public async Task<bool> UpdateUser(string id, Action<ApplicationUser> updateValues)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            updateValues(user);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetDefaultProfilePicture(string id, string picture)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            user.FileName = picture;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool IsUserManagerOfMusician(string userId, int musicianId)
            => _dbContext.Musicians.Any(x => x.Id == musicianId && x.ManagerId == userId);

        public IEnumerable<T> GetUsersByName<T>(string name, int count, Func<ApplicationUser, T> selector)
        => _dbContext.Users.AsEnumerable().
            Where(x => x.UserName.IsSimiliar(name)).
            Select(x => new { User = x, Distance = x.UserName.LevenshteinDistance(name) }).
            OrderBy(x => x.Distance).
            Take(count).
            Select(x => x.User).
            Select(selector).
            AsEnumerable();
    }
}
