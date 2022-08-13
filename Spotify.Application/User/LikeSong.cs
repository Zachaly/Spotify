
namespace Spotify.Application.User
{
    [Service]
    public class LikeSong
    {
        private readonly IApplicationUserManager _applicationUserManager;

        public LikeSong(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        /// <summary>
        /// Adds song like
        /// </summary>
        public async Task<bool> Execute(string userId, int songId)
            => await _applicationUserManager.LikeSong(userId, songId);

    }
}
