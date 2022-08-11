using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IPlaylistManager
    {
        Task<bool> AddPlaylist(Playlist playlist);
        T GetPlaylist<T>(int id, Func<Playlist, T> selector);
        IEnumerable<T> GetUserPlaylists<T>(string userId, Func<Playlist, T> selector);
        Task<bool> RemovePlaylist(int id);
        Task<bool> AddSongToPlaylist(int songId, int playlistId);
        Task<bool> RemoveSongFromPlaylist(int songId, int playlistId);
        Task<bool> UpdatePlaylist(int id, Action<Playlist> changes);
        bool DoesPlaylistContainSong(int playlistId, int songId);
        Task<bool> SetCoverPicture(int id, string filename);
        IEnumerable<T> GetPlaylistsByName<T>(string name, int count, Func<Playlist, T> selector);
    }
}
