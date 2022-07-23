
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class AddMusician
    {
        private IMusicianManager _musicianManager;

        public AddMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _musicianManager.AddMusicianAsync(new Musician 
            { 
                Name = request.Name,
                Description = request.Description
            });
        
        public class Request 
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
