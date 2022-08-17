
namespace Spotify.Application.User
{
    [Service]
    public class GetUserProfile
    {
        private readonly IApplicationUserManager _applicationUserManager;

        public GetUserProfile(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        /// <summary>
        /// Gets user info used in user profile
        /// </summary>
        public Response Execute(string id) => _applicationUserManager.GetUserById(id, user => new Response
        {
            Name = user.UserName,
            Id = user.Id,
            FollowedMusicians = user.FollowedMusicians.Select(follow => new MusicianModel
            {
                Id = follow.MusicianId,
                Name = follow.Musician.Name,
            }),
            LikedAlbums = user.LikedAlbums.Select(like => new AlbumModel
            {
                CreatorName = like.Album.Musician.Name,
                Name = like.Album.Name,
                Id = like.AlbumId,
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
