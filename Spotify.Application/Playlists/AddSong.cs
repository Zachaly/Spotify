
namespace Spotify.Application.Playlists
{
    [Service]
    public class AddSong
    {
        private IPlaylistManager _playlistManager;

        public AddSong(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public async Task<bool> Execute(int songId, int playlistId) 
            => await _playlistManager.AddSongToPlaylist(songId, playlistId);

    }
}
