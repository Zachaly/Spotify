
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

        public async Task<bool> Execute(Request request)
            => await _musicianManager.UpdateMusicianAsync(request.Id, request.Name, request.Description);

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
