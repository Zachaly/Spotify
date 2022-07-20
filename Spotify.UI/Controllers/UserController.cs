using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<ApplicationUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
