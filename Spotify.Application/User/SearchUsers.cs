
namespace Spotify.Application.User
{
    [Service]
    public class SearchUsers
    {
        private readonly IApplicationUserManager _appUserManager;

        public SearchUsers(IApplicationUserManager applicationUserManager)
        {
            _appUserManager = applicationUserManager;
        }

        /// <summary>
        /// Searches for users with similar name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> Execute(string name)
            => _appUserManager.GetUsersByName(name, 10, user => new UserModel
            {
                Id = user.Id,
                UserName = user.UserName
            });

        public class UserModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
