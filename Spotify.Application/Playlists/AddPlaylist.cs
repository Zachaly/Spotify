
namespace Spotify.Application.Playlists
{
    [Service]
    public class AddPlaylist
    {
        private IPlaylistManager _playlistManager;

        public AddPlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _playlistManager.AddPlaylist(new Playlist 
            { 
                CreatorId = request.UserId,
                Name = request.Name,
                FileName = request.FileName,
            });

        public class Request
        {
            public string Name { get; set; }
            public string UserId { get; set; }
            public string FileName { get; set; }
        }
    }
}
