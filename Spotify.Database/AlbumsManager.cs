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

        public async Task<bool> RemoveAlbumAsync(int id)
        {
            var album = _dbContext.Albums.FirstOrDefault(x => x.Id == id);

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
