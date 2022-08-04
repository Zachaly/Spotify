
namespace Spotify.Application.Playlists
{
    [Service]
    public class RemovePlaylist
    {
        private IPlaylistManager _playlistManager;

        public RemovePlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public async Task<bool> Execute(int id) => await _playlistManager.RemovePlaylist(id);
    }
}
