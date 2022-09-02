using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly AppDbContext _dbContext;

        public ApplicationUserManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ApplicationUser GetUserByEmail(string email) 
            => _dbContext.Users.FirstOrDefault(user => user.Email == email);

        public bool IsEmailOccupied(string email)
            => _dbContext.Users.Any(user => user.Email == email);

        public T GetUserById<T>(string id, Func<ApplicationUser, T> selector)
            => _dbContext.Users.Include(user => user.FollowedMusicians).ThenInclude(follow => follow.Musician).
                Include(user => user.LikedAlbums).ThenInclude(like => like.Album).
                Include(user => user.LikedAlbums).ThenInclude(like => like.Album).ThenInclude(album => album.Musician).
                Include(user => user.LikedSongs).
                Where(user => user.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetUserLikedSongs<T>(string userId, Func<SongLike, T> selector)
            => _dbContext.Users.
                Include(user => user.LikedSongs).ThenInclude(like => like.Song).ThenInclude(song => song.Album).
                Include(user => user.LikedSongs).ThenInclude(like => like.Song).ThenInclude(song => song.Creator).
                FirstOrDefault(user => user.Id == userId)?.
                LikedSongs.Select(selector);

        public async Task<bool> FollowMusician(string userId, int musicianId)
        {
            var musicianFollow = new MusicianFollow
            {
                ApplicationUserId = userId,
                MusicianId = musicianId,
            };

            var userFollows = _dbContext.MusicianFollows.Where(x => x.ApplicationUserId == userId);

            if(userFollows.Any(follow => follow.MusicianId == musicianId))
                _dbContext.MusicianFollows.Remove(userFollows.FirstOrDefault(x => x.MusicianId == musicianId));
            else
                _dbContext.MusicianFollows.Add(musicianFollow);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> LikeSong(string userId, int songId)
        {
            var songLike = new SongLike
            {
                ApplicationUserId = userId,
                SongId = songId,
            };

            var userLikes = _dbContext.SongLikes.Where(x => x.ApplicationUserId == userId);

            if (userLikes.Any(like => like.SongId == songId))
                _dbContext.SongLikes.Remove(userLikes.FirstOrDefault(x => x.SongId == songId));
            else
                _dbContext.SongLikes.Add(songLike);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> LikeAlbum(string userId, int albumId)
        {
            var albumLike = new AlbumLike
            {
                ApplicationUserId = userId,
                AlbumId = albumId,
            };

            var userLikes = _dbContext.AlbumLikes.Where(x => x.ApplicationUserId == userId);

            if (userLikes.Any(like => like.AlbumId == albumId))
                _dbContext.AlbumLikes.Remove(userLikes.FirstOrDefault(x => x.AlbumId == albumId));
            else
                _dbContext.AlbumLikes.Add(albumLike);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool IsSongLiked(string userId, int songId)
            => _dbContext.SongLikes.Any(like => like.SongId == songId && like.ApplicationUserId == userId);

        public bool IsMusicianFollowed(string userId, int musicianId)
            => _dbContext.MusicianFollows.Any(follow => follow.MusicianId == musicianId && follow.ApplicationUserId == userId);

        public bool IsAlbumLiked(string userId, int albumId)
            => _dbContext.AlbumLikes.Any(like => like.AlbumId == albumId && like.ApplicationUserId == userId);

        public async Task<bool> UpdateUser(string id, Action<ApplicationUser> updateValues)
        {
            var updatedUser = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            updateValues(updatedUser);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetDefaultProfilePicture(string id, string picture)
        {
            var updatedUser = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            updatedUser.FileName = picture;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool IsUserManagerOfMusician(string userId, int musicianId)
            => _dbContext.Musicians.Any(musician => musician.Id == musicianId && musician.ManagerId == userId);

        public IEnumerable<T> GetUsersByName<T>(string name, int count, Func<ApplicationUser, T> selector)
        => _dbContext.Users.AsEnumerable().
            Where(user => user.UserName.IsSimiliar(name)).
            Select(user => new { User = user, Distance = user.UserName.LevenshteinDistance(name) }).
            OrderBy(userWithDistance => userWithDistance.Distance).
            Take(count).
            Select(userWithDistance => userWithDistance.User).
            Select(selector).
            AsEnumerable();
    }
}
