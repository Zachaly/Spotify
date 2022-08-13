using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class AlbumsManager : IAlbumsManager
    {
        private readonly AppDbContext _dbContext;

        public AlbumsManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAlbumAsync(Album album)
        {
            _dbContext.Albums.Add(album);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        private IEnumerable<Album> GetAlbums()
            => _dbContext.Albums.Include(album => album.Songs).
                Include(album => album.Musician).
                AsEnumerable();// used to avoid exeptions

        private IEnumerable<Album> GetAlbumsWithCondition(Func<Album, bool> condition)
            => GetAlbums().Where(album => condition(album));

        public T GetAlbumById<T>(int id, Func<Album, T> selector)
            => GetAlbumsWithCondition(album => album.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetAlbums<T>(Func<Album, T> selector)
            => GetAlbums().Select(selector);


        public IEnumerable<T> GetAlbumsByName<T>(string name, int count, Func<Album, T> selector)
            => GetAlbumsWithCondition(album => album.Name.IsSimiliar(name)).
                Select(album => new { Album = album, Distance = album.Name.LevenshteinDistance(name) }).
                OrderBy(albumWithDistance => albumWithDistance.Distance).
                Take(count).
                Select(albumWithDistance => albumWithDistance.Album).
                Select(selector);

        public IEnumerable<T> GetAlbumsOfManager<T>(string managerId, Func<Album, T> selector)
            => GetAlbumsWithCondition(album => album.Musician.ManagerId == managerId).
                OrderByDescending(album => album.Id).
                Select(selector);

        public IEnumerable<T> GetAlbumsOfMusician<T>(int musicianId, Func<Album, T> selector)
            => GetAlbumsWithCondition(album => album.MusicianId == musicianId).
                OrderByDescending(album => album.Id).
                Select(selector);

        public IEnumerable<T> GetTopAlbums<T>(int creatorId, int count, Func<Album, T> selector)
            => GetAlbumsWithCondition(album => album.MusicianId == creatorId).
                OrderByDescending(album => album.Songs.Sum(song => song.Plays)).
                Take(count).
                Select(selector);

        public async Task<bool> RemoveAlbumAsync(int id)
        {
            var removedAlbum = _dbContext.Albums.FirstOrDefault(album => album.Id == id);

            _dbContext.AlbumLikes.RemoveRange(_dbContext.AlbumLikes.Where(like => like.AlbumId == id).ToList());

            _dbContext.Songs.RemoveRange(_dbContext.Songs.Where(song => song.AlbumId == id).ToList());

            _dbContext.Albums.Remove(removedAlbum);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAlbumAsync(int id, Action<Album> changedValues)
        {
            var updatedAlbum = _dbContext.Albums.FirstOrDefault(album => album.Id == id);

            changedValues(updatedAlbum);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
