
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class UpdateSong
    {
        private readonly ISongsManager _songsManager;

        public UpdateSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        /// <summary>
        /// Updates song with data given in request
        /// </summary>
        public async Task<Response> Execute(Request request)
        { 
            await _songsManager.UpdateSongAsync(request.Id, song =>
            {
                song.Name = request.Name;
            });

            return _songsManager.GetSongById(request.Id, song => new Response
            {
                Id = song.Id,
                Name = song.Name,
                Plays = song.Plays
            });
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }
    }
}
