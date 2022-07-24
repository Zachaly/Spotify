
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class DeleteMusician
    {
        private IMusicianManager _musicianManager;

        public DeleteMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public async Task<bool> Execute(int id) => await _musicianManager.DeleteMusicianAsync(id);
    }
}
