using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers.Manager
{
    public abstract class ManagerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserManager _appUserManager;

        public ManagerController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager) 
        {
            _userManager = userManager;
            _appUserManager = appUserManager;
        }

        protected string GetId() => _userManager.GetUserId(HttpContext.User);
        protected bool IsManagerCorrect(int musicianId) 
            => _appUserManager.IsUserManagerOfMusician(GetId(), musicianId);
    }
}
