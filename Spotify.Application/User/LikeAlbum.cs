
namespace Spotify.Application.User
{
    [Service]
    public class LikeAlbum
    {
        private readonly IApplicationUserManager _applicationUserManager;

        public LikeAlbum(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        /// <summary>
        /// Adds album like
        /// </summary>
        public async Task<bool> Execute(string userId, int albumId)
            => await _applicationUserManager.LikeAlbum(userId, albumId);
    }
}
