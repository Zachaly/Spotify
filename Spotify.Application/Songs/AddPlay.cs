namespace Spotify.Application.Songs
{
    [Service]
    public class AddPlay
    {
        private ISongsManager _songsManager;

        public AddPlay(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public async Task<bool> Execute(int id) => await _songsManager.AddPlay(id);
    }
}
