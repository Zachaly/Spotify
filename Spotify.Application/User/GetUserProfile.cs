
namespace Spotify.Application.User
{
    [Service]
    public class GetUserProfile
    {
        private IApplicationUserManager _applicationUserManager;

        public GetUserProfile(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public Response Execute(string id) => _applicationUserManager.GetUserById(id, user => new Response
        {
            Name = user.UserName,
            Id = user.Id,
            FollowedMusicians = user.FollowedMusicians.Select(musician => new MusicianModel
            {
                Id = musician.MusicianId,
                Name = musician.Musician.Name
            }),
            LikedAlbums = user.LikedAlbums.Select(album => new AlbumModel
            {
                CreatorName = album.Album.Musician.Name,
                Name = album.Album.Name,
                Id = album.AlbumId
            }),
            LikedSongsCount = user.LikedSongs.Count()
        });

        public class Response
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<MusicianModel> FollowedMusicians { get; set; }
            public IEnumerable<AlbumModel> LikedAlbums { get; set; }
            public int LikedSongsCount { get; set; }
        }

        public class MusicianModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class AlbumModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
        }
    }
}
