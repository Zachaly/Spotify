using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class SongsManager : ISongsManager
    {
        private readonly AppDbContext _dbContext;

        public SongsManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IEnumerable<Song> GetSongs()
            => _dbContext.Songs.
                Include(song => song.Album).
                Include(song => song.Creator).
                AsEnumerable();

        private IEnumerable<Song> GetSongsWithCondition(Func<Song, bool> condition)
            => GetSongs().Where(song => condition(song));

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
            => GetSongsWithCondition(song => song.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetSongs<T>(Func<Song, T> selector)
            => GetSongs().
                Select(selector);

        public IEnumerable<T> GetSongsOfManager<T>(string id, Func<Song, T> selector)
            => GetSongsWithCondition(song => song.Creator.ManagerId == id).
                Select(selector);

        public IEnumerable<T> GetTopSongs<T>(int creatorId, int count, Func<Song, T> selector)
            => GetSongsWithCondition(song => song.Creator.Id == creatorId).
                OrderByDescending(song => song.Plays).
                Take(count).
                Select(selector);

        public async Task<bool> RemoveSongAsync(int id)
        {
            var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);

            _dbContext.SongLikes.RemoveRange(_dbContext.SongLikes.Where(like => like.SongId == id).ToList());

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
            => GetSongsWithCondition(song => song.Name.IsSimiliar(name)).
                Select(song => new { Song = song, Distance = song.Name.LevenshteinDistance(name) }).
                OrderBy(songWithDistance => songWithDistance.Distance).
                Take(count).
                Select(songWithDistance => songWithDistance.Song).
                Select(selector);
    }
}
