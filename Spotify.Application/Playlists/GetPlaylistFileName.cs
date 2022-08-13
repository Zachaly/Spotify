
namespace Spotify.Application.Playlists
{
    [Service]
    public class GetPlaylistFileName
    {
        private readonly IPlaylistManager _playlistManager;

        public GetPlaylistFileName(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Gets name of file containing playlist cover
        /// </summary>
        public string Execute(int id) => _playlistManager.GetPlaylist(id, playlist => playlist.FileName);
    }
}
