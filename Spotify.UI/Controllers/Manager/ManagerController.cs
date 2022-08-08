using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.UI.Controllers.Manager
{
    public abstract class ManagerController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IApplicationUserManager _appUserManager;
        private UserManager<ApplicationUser> userManager;

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
