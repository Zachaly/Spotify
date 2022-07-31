
namespace Spotify.Application.User
{
    [Service]
    public class GetUserLikedSongs
    {
        private IApplicationUserManager _applicationUserManager;

        public GetUserLikedSongs(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public IEnumerable<SongModel> Execute(string userId) => _applicationUserManager.GetUserLikedSongs(userId, song => new SongModel
        {
            Id = song.SongId,
            CreatorId = song.Song.MusicianId,
            CreatorName = song.Song.Creator.Name,
            Name = song.Song.Name,
            AlbumId = song.Song.AlbumId,
            FileName = song.Song.FileName
        });

        public class SongModel
        {
            public int Id { get; set; }
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public int AlbumId { get; set; }
            public string FileName { get; set; }
        }
    }
}
