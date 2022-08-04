
namespace Spotify.Application.Playlists
{
    [Service]
    public class SetCoverPicture
    {
        private IPlaylistManager _playlistManager;

        public SetCoverPicture(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public async Task<bool> Execute(int id, string fileName) 
            => await _playlistManager.SetCoverPicture(id, fileName);
    }
}
