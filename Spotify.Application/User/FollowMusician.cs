
namespace Spotify.Application.User
{
    [Service]
    public class FollowMusician
    {
        private IApplicationUserManager _applicationUserManager;

        public FollowMusician(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        /// <summary>
        /// Adds a follow
        /// </summary>
        public async Task<bool> Execute(string userId, int musicianId)
            => await _applicationUserManager.FollowMusician(userId, musicianId);

    }
}
