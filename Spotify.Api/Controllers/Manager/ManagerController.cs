using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Api.Controllers.Manager
{
    public abstract class ManagerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserManager _appUserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManagerController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _appUserManager = appUserManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetId() => _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);
        protected bool IsManagerCorrect(int musicianId) 
            => _appUserManager.IsUserManagerOfMusician(GetId(), musicianId);
    }
}
