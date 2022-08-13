
namespace Spotify.Application.Playlists
{
    [Service]
    public class SetCoverPicture
    {
        private readonly IPlaylistManager _playlistManager;

        public SetCoverPicture(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Sets cover picture of given playlist
        /// </summary>
        public async Task<bool> Execute(int id, string fileName) 
            => await _playlistManager.SetCoverPicture(id, fileName);
    }
}
