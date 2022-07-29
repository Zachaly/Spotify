using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.User
{
    [Service]
    public class LikeAlbum
    {
        private IApplicationUserManager _applicationUserManager;

        public LikeAlbum(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public async Task<bool> Execute(string userId, int albumId)
            => await _applicationUserManager.LikeAlbum(userId, albumId);
    }
}
