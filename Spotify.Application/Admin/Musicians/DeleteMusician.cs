
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class DeleteMusician
    {
        private readonly IMusicianManager _musicianManager;

        public DeleteMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Removes musician with given id
        /// </summary>
        public async Task<bool> Execute(int id) => await _musicianManager.DeleteMusicianAsync(id);
    }
}
