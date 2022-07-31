using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class AlbumsManager : IAlbumsManager
    {
        private AppDbContext _dbContext;

        public AlbumsManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAlbumAsync(Album album)
        {
            _dbContext.Albums.Add(album);

            return await _dbContext.SaveChangesAsync() > 0;
        }


        public T GetAlbumById<T>(int id, Func<Album, T> selector)
            => _dbContext.Albums.Include(db => db.Songs).Include(db => db.Musician).
                Where(album => album.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetAlbums<T>(Func<Album, T> selector)
            => _dbContext.Albums.Include(db => db.Songs).Include(db => db.Musician).
            Select(selector).
            AsEnumerable();

        public IEnumerable<T> GetAlbumsOfMusician<T>(int musicianId, Func<Album, T> selector)
            => _dbContext.Albums.Include(db => db.Musician).Include(db => db.Songs).
                Where(album => album.MusicianId == musicianId).
                OrderByDescending(album => album.Id).
                Select(selector).
                ToList();

        public IEnumerable<T> GetTopAlbums<T>(int creatorId, int count, Func<Album, T> selector)
            => _dbContext.Albums.Include(db => db.Musician).Include(db => db.Songs).
                Where(album => album.MusicianId == creatorId).
                OrderByDescending(album => album.Songs.Sum(song => song.Plays)).
                Take(count).
                Select(selector);

        public async Task<bool> RemoveAlbumAsync(int id)
        {
            var album = _dbContext.Albums.FirstOrDefault(x => x.Id == id);

            _dbContext.AlbumLikes.RemoveRange(_dbContext.AlbumLikes.Where(x => x.AlbumId == id).ToList());

            _dbContext.Albums.Remove(album);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAlbumAsync(int id, Action<Album> changedValues)
        {
            var album = _dbContext.Albums.FirstOrDefault(x => x.Id == id);

            changedValues(album);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
