using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class PlaylistManager : IPlaylistManager
    {
        private AppDbContext _dbContext;

        public PlaylistManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddPlaylist(Playlist playlist)
        {
            _dbContext.Playlists.Add(playlist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        private IEnumerable<Playlist> GetPlaylists() 
            => _dbContext.Playlists.Include(x => x.Songs).ThenInclude(x => x.Song).ThenInclude(x => x.Album).
                Include(x => x.Songs).ThenInclude(x => x.Song).ThenInclude(x => x.Creator).
                Include(x => x.Creator).
                AsEnumerable();

        public async Task<bool> AddSongToPlaylist(int songId, int playlistId)
        {
            var playlistSong = new PlaylistSong
            {
                SongId = songId,
                PlaylistId = playlistId
            };

            _dbContext.PlaylistSongs.Add(playlistSong);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public T GetPlaylist<T>(int id, Func<Playlist, T> selector)
            => GetPlaylists().
                Where(x => x.Id == id).
                Select(selector).FirstOrDefault();

        public IEnumerable<T> GetUserPlaylists<T>(string userId, Func<Playlist, T> selector)
            => GetPlaylists().
                Where(x => x.CreatorId == userId).
                Select(selector);

        public async Task<bool> RemovePlaylist(int id)
        {
            var playlist = _dbContext.Playlists.FirstOrDefault(x => x.Id == id);

            _dbContext.Playlists.Remove(playlist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveSongFromPlaylist(int songId, int playlistId)
        {
            var playlistSong = _dbContext.PlaylistSongs.FirstOrDefault(x => x.SongId == songId && x.PlaylistId == playlistId);

            _dbContext.PlaylistSongs.Remove(playlistSong);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePlaylist(int id, Action<Playlist> changes)
        {
            var playlist = _dbContext.Playlists.FirstOrDefault(x => x.Id == id);

            changes(playlist);

            _dbContext.Playlists.Update(playlist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool DoesPlaylistContainSong(int playlistId, int songId)
            => _dbContext.PlaylistSongs.Any(x => x.PlaylistId == playlistId && x.SongId == songId);

        public async Task<bool> SetCoverPicture(int id, string filename)
        {
            var playlist = _dbContext.Playlists.FirstOrDefault(x => x.Id == id);

            playlist.FileName = filename;
            _dbContext.Playlists.Update(playlist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IEnumerable<T> GetPlaylistsByName<T>(string name, int count, Func<Playlist, T> selector)
        => _dbContext.Playlists.Include(x => x.Creator).AsEnumerable().
            Where(x => x.Name.IsSimiliar(name)).
            Select(x => new { Playlist = x, Distance = x.Name.LevenshteinDistance(name) }).
            OrderBy(x => x.Distance).
            Take(count).
            Select(x => x.Playlist).
            Select(selector).
            AsEnumerable();
    }
}
