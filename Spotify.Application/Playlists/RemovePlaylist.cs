
namespace Spotify.Application.Playlists
{
    [Service]
    public class RemovePlaylist
    {
        private readonly IPlaylistManager _playlistManager;

        public RemovePlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Removes playlist from database
        /// </summary>
        public async Task<bool> Execute(int id) => await _playlistManager.RemovePlaylist(id);
    }
}
