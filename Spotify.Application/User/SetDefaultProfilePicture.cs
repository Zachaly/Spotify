
namespace Spotify.Application.User
{
    [Service]
    public class SetDefaultProfilePicture
    {
        private readonly IApplicationUserManager _appUserManager;

        public SetDefaultProfilePicture(IApplicationUserManager applicationUserManager)
        {
            _appUserManager = applicationUserManager;
        }

        public async Task<bool> Execute(string id) => await _appUserManager.SetDefaultProfilePicture(id, "placeholder.jpg");
    }
}
