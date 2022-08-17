
namespace Spotify.Application.Playlists
{
    [Service]
    public class GetUserPlaylists
    {
        private readonly IPlaylistManager _playlistManager;

        public GetUserPlaylists(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Gets playlists of given user
        /// </summary>
        public IEnumerable<PlaylistViewModel> Execute(string userId)
            => _playlistManager.GetUserPlaylists(userId, playlist => new PlaylistViewModel
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    SongCount = playlist.Songs.Count(),
                    CreatorName = playlist.Creator.UserName
                });

        public class PlaylistViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
            public int SongCount { get; set; }
        }
    }
}
