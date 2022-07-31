using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Songs;
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

        [HttpPost("/AddPlay/{id}")]
        public async Task<IActionResult> AddPlay(int id, [FromServices] AddPlay addPlay)
            => Ok(await addPlay.Execute(id));
    }
}
