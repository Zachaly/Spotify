
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class AddSong
    {
        private readonly ISongsManager _songsManager;

        public AddSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        /// <summary>
        /// Adds song and returns model used in admin panel
        /// </summary>
        public async Task<Response> Execute(Request request)
        {
            var song = new Song
            {
                Name = request.Name,
                AlbumId = request.AlbumId,
                MusicianId = request.CreatorId,
                FileName = request.FileName
            };
            await _songsManager.AddSongAsync(song);

            return new Response
            {
                Id = song.Id,
                Name = song.Name,
                Plays = 0,
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public int CreatorId { get; set; }
            public int AlbumId { get; set; }
            public string FileName { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }
    }
}
