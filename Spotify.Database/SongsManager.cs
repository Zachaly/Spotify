using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class SongsManager : ISongsManager
    {
        private AppDbContext _dbContext;

        public SongsManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddSongAsync(Song song)
        {
            _dbContext.Songs.Add(song);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public T GetSongById<T>(int id, Func<Song, T> selector)
            => _dbContext.Songs.Include(song => song.Album).Include(song => song.Creator).
            Where(song => song.Id == id).
            Select(selector).
            FirstOrDefault();

        public IEnumerable<T> GetSongs<T>(Func<Song, T> selector)
            => _dbContext.Songs.Include(song => song.Album).Include(song => song.Creator).
            Select(selector).
            AsEnumerable();

        public async Task<bool> RemoveSongAsync(int id)
        {
            var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);

            _dbContext.Songs.Remove(song);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSongAsync(int id, Action<Song> updateValues)
        {
            var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);

            updateValues(song);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
