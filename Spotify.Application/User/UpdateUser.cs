
namespace Spotify.Application.User
{
    [Service]
    public class UpdateUser
    {
        private readonly IApplicationUserManager _appUserManager;

        public UpdateUser(IApplicationUserManager applicationUserManager)
        {
            _appUserManager = applicationUserManager;
        }

        public async Task<bool> Execute(Request request) => await _appUserManager.UpdateUser(request.Id, user =>
        {
            user.UserName = request.UserName;
            if (!string.IsNullOrEmpty(request.FileName))
                user.FileName = request.FileName;
        });

        public class Request
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FileName { get; set; }
        }
    }
}
