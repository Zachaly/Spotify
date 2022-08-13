
namespace Spotify.Application.User
{
    [Service]
    public class GetProfilePictureFileName
    {
        private readonly IApplicationUserManager _appUserManager;

        public GetProfilePictureFileName(IApplicationUserManager applicationUserManager)
        {
            _appUserManager = applicationUserManager;
        }

        public string Execute(string id) => _appUserManager.GetUserById(id, user => user.FileName);
    }
}
