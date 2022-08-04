
namespace Spotify.Application.Playlists
{
    [Service]
    public class UpdatePlaylist
    {
        private IPlaylistManager _playlistManager;

        public UpdatePlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _playlistManager.UpdatePlaylist(request.Id, playlist =>
                {
                    playlist.Name = request.Name;
                    playlist.FileName = request.FileName;
                });

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
        }
    }
}
