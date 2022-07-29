using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.User
{
    [Service]
    public class LikeSong
    {
        private IApplicationUserManager _applicationUserManager;

        public LikeSong(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public async Task<bool> Execute(string userId, int songId)
            => await _applicationUserManager.LikeSong(userId, songId);

    }
}
