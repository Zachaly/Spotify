
namespace Spotify.Application.User
{
    [Service]
    public class GetUserLikedSongs
    {
        private readonly IApplicationUserManager _applicationUserManager;

        public GetUserLikedSongs(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        /// <summary>
        /// Gets info about all songs liked by user needed to play show them
        /// </summary>
        public IEnumerable<SongModel> Execute(string userId) => _applicationUserManager.GetUserLikedSongs(userId, song => new SongModel
        {
            Id = song.SongId,
            CreatorId = song.Song.MusicianId,
            CreatorName = song.Song.Creator.Name,
            Name = song.Song.Name,
            AlbumId = song.Song.AlbumId,
        });

        public class SongModel
        {
            public int Id { get; set; }
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public int AlbumId { get; set; }
        }
    }
}
