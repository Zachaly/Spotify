using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly AppDbContext _dbContext;

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
            => _dbContext.Playlists.
                Include(playlist => playlist.Songs).ThenInclude(song => song.Song).ThenInclude(song => song.Album).
                Include(playlist => playlist.Songs).ThenInclude(song => song.Song).ThenInclude(song => song.Creator).
                Include(playlist => playlist.Creator).
                AsEnumerable();

        private IEnumerable<Playlist> GetPlaylistsWithCondition(Func<Playlist, bool> condition)
            => GetPlaylists().Where(playlist => condition(playlist));

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
            => GetPlaylistsWithCondition(playlist => playlist.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetUserPlaylists<T>(string userId, Func<Playlist, T> selector)
            => GetPlaylistsWithCondition(playlist => playlist.CreatorId == userId).
                Select(selector);

        public async Task<bool> RemovePlaylist(int id)
        {
            var removedPlaylist = _dbContext.Playlists.FirstOrDefault(playlist => playlist.Id == id);

            _dbContext.Playlists.Remove(removedPlaylist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveSongFromPlaylist(int songId, int playlistId)
        {
            var playlistSong = _dbContext.PlaylistSongs.FirstOrDefault(song => song.SongId == songId && song.PlaylistId == playlistId);

            _dbContext.PlaylistSongs.Remove(playlistSong);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePlaylist(int id, Action<Playlist> changes)
        {
            var updatedPlaylist = _dbContext.Playlists.FirstOrDefault(playlist => playlist.Id == id);

            changes(updatedPlaylist);

            _dbContext.Playlists.Update(updatedPlaylist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool DoesPlaylistContainSong(int playlistId, int songId)
            => _dbContext.PlaylistSongs.Any(playlist => playlist.PlaylistId == playlistId && playlist.SongId == songId);

        public async Task<bool> SetCoverPicture(int id, string filename)
        {
            var updatedPlaylist = _dbContext.Playlists.FirstOrDefault(playlist => playlist.Id == id);

            updatedPlaylist.FileName = filename;
            _dbContext.Playlists.Update(updatedPlaylist);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IEnumerable<T> GetPlaylistsByName<T>(string name, int count, Func<Playlist, T> selector)
            => _dbContext.Playlists.Include(playlist => playlist.Creator).AsEnumerable().
                Where(playlist => playlist.Name.IsSimiliar(name)).
                Select(playlist => new { Playlist = playlist, Distance = playlist.Name.LevenshteinDistance(name) }).
                OrderBy(playlistWithDistance => playlistWithDistance.Distance).
                Take(count).
                Select(playlistWithDistance => playlistWithDistance.Playlist).
                Select(selector);
    }
}
