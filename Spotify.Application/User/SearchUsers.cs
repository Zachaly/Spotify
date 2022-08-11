using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.User
{
    [Service]
    public class SearchUsers
    {
        private IApplicationUserManager _appUserManager;

        public SearchUsers(IApplicationUserManager applicationUserManager)
        {
            _appUserManager = applicationUserManager;
        }

        public IEnumerable<UserModel> Execute(string name)
            => _appUserManager.GetUsersByName(name, 10, user => new UserModel
            {
                FileName = user.FileName,
                Id = user.Id,
                UserName = user.UserName
            });

        public class UserModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FileName { get; set; }
        }
    }
}
