
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class GetSongFileName
    {
        private readonly ISongsManager _songsManager;

        public GetSongFileName(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public string Execute(int id) => _songsManager.GetSongById(id, song => song.FileName);
    }
}
