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

        public async Task<bool> AddPlay(int id)
        {
            var song = _dbContext.Songs.FirstOrDefault(x => x.Id == id);

            song.Plays++;

            return await _dbContext.SaveChangesAsync() > 0;
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

        public IEnumerable<T> GetSongsOfManager<T>(string id, Func<Song, T> selector)
            => _dbContext.Songs.Include(song => song.Album).Include(song => song.Creator).
                Where(song => song.Creator.ManagerId == id).
                Select(selector).
                AsEnumerable();

        public IEnumerable<T> GetTopSongs<T>(int creatorId, int count, Func<Song, T> selector)
            => _dbContext.Songs.Include(db => db.Creator).Include(db => db.Album).
                Where(song => song.Creator.Id == creatorId).
                OrderByDescending(song => song.Plays).
                Take(count).
                Select(selector).
                AsEnumerable();

        public async Task<bool> RemoveSongAsync(int id)
        {
            var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);

            _dbContext.SongLikes.RemoveRange(_dbContext.SongLikes.Where(x => x.SongId == id).ToList());

            _dbContext.Songs.Remove(song);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSongAsync(int id, Action<Song> updateValues)
        {
            var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);

            updateValues(song);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IEnumerable<T> GetSongsByName<T>(string name, int count, Func<Song, T> selector)
            => _dbContext.Songs.Include(x => x.Album).Include(x => x.Creator).AsEnumerable().
            Where(x => x.Name.IsSimiliar(name)).
            Select(x => new { Song = x, Distance = x.Name.LevenshteinDistance(name) }).
            OrderBy(x => x.Distance).
            Take(count).
            Select(x => x.Song).
            Select(selector).
            AsEnumerable();
    }
}
