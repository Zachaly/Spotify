
namespace Spotify.Application.Playlists
{
    [Service]
    public class AddPlaylist
    {
        private readonly IPlaylistManager _playlistManager;

        public AddPlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Adds playlist to database
        /// </summary>
        public async Task<Response> Execute(Request request)
        {
            var playlist = new Playlist
            {
                CreatorId = request.UserId,
                Name = request.Name,
                FileName = request.FileName,
            };
            await _playlistManager.AddPlaylist(playlist);

            return _playlistManager.GetPlaylist(playlist.Id, x => new Response
            {
                Name = x.Name,
                CreatorName = x.Creator.UserName,
                Id = x.Id,
                SongCount = x.Songs.Count()
            });
        }

        public class Request
        {
            public string Name { get; set; }
            public string UserId { get; set; }
            public string FileName { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
            public int SongCount { get; set; }
        }
    }
}
