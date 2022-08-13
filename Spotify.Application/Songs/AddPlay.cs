namespace Spotify.Application.Songs
{
    [Service]
    public class AddPlay
    {
        private readonly ISongsManager _songsManager;

        public AddPlay(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        /// <summary>
        /// Adds one play to a song
        /// </summary>
        public async Task<bool> Execute(int id) => await _songsManager.AddPlay(id);
    }
}
