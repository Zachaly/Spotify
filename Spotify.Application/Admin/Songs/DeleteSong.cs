
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class DeleteSong
    {
        private readonly ISongsManager _songsManager;

        public DeleteSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public async Task<bool> Execute(int id) => await _songsManager.RemoveSongAsync(id);
    }
}
