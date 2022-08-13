
namespace Spotify.Application.Playlists
{
    [Service]
    public class UpdatePlaylist
    {
        private readonly IPlaylistManager _playlistManager;

        public UpdatePlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Updates playlist with info given in request
        /// </summary>
        public async Task<bool> Execute(Request request) 
            => await _playlistManager.UpdatePlaylist(request.Id, playlist =>
                {
                    playlist.Name = request.Name;
                    if(!string.IsNullOrEmpty(request.FileName))
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
