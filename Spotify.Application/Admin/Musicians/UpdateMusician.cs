
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class UpdateMusician
    {
        private IMusicianManager _musicianManager;

        public UpdateMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Updates musician with data given in request and returns model used in admin panel
        /// </summary>
        public async Task<Response> Execute(Request request)
        { 
            await _musicianManager.UpdateMusicianAsync(request.Id, request.Name, request.Description);

            return _musicianManager.GetMusicianById(request.Id, musician => new Response
            {
                Id = musician.Id,
                Name = musician.Name,
                NumberOfFollowers = musician.Followers.Count(),
                NumberOfPlays = musician.Songs.Sum(song => song.Plays),
                NumberOfSongs = musician.Songs.Count()
            });
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Response
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public int NumberOfSongs { get; set; }
            public int NumberOfFollowers { get; set; }
            public long NumberOfPlays { get; set; }
        }
    }
}
