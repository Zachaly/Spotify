
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class AddMusician
    {
        private readonly IMusicianManager _musicianManager;

        public AddMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Adds musician and returns model used in admin panel
        /// </summary>
        public async Task<Response> Execute(Request request)
        {
            var musician = new Musician
            {
                Name = request.Name,
                Description = request.Description,
                FileName = request.FileName,
                ManagerId = request.ManagerId
            };

            await _musicianManager.AddMusicianAsync(musician);

            return new Response
            {
                Name = musician.Name,
                Description = musician.Description,
                Id = musician.Id,
                NumberOfFollowers = 0,
                NumberOfPlays = 0,
                NumberOfSongs = 0
            };
        }
        
        public class Request 
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string FileName { get; set; }
            public string? ManagerId { get; set; }
        }

        public class Response
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
            public int NumberOfSongs { get; set; }
            public int NumberOfFollowers { get; set; }
            public long NumberOfPlays { get; set; }
        }
    }
}
