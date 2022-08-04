
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

        public async Task<bool> Execute(int songId, int playlistId) 
            => await _playlistManager.RemoveSongFromPlaylist(songId, playlistId);

    }
}
