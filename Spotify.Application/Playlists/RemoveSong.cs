
namespace Spotify.Application.Playlists
{
    [Service]
    public class RemoveSong
    {
        private IPlaylistManager _playlistManager;

        public RemoveSong(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Removes song from playlist
        /// </summary>
        public async Task<bool> Execute(int songId, int playlistId) 
            => await _playlistManager.RemoveSongFromPlaylist(songId, playlistId);

    }
}
