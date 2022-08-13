
namespace Spotify.Application.Playlists
{
    [Service]
    public class SearchPlaylists
    {
        private readonly IPlaylistManager _playlistManager;

        public SearchPlaylists(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Searches for playlists with similar name
        /// </summary>
        public IEnumerable<PlaylistModel> Execute(string name)
            => _playlistManager.GetPlaylistsByName(name, 10, playlist => new PlaylistModel
            {
                FileName = playlist.FileName,
                Id = playlist.Id,
                Name = playlist.Name
            });

        public class PlaylistModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
        }
    }
}
