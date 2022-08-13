
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class GetMusicianFileName
    {
        private readonly IMusicianManager _musicianManager;

        public GetMusicianFileName(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public string Execute(int id) => _musicianManager.GetMusicianById(id, musician => musician.FileName);
    }
}
