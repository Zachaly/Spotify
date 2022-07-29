using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> Execute(string userId, int musicianId)
            => await _applicationUserManager.FollowMusician(userId, musicianId);

    }
}
