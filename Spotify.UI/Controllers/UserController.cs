using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Songs;
using Spotify.Application.User;
using Spotify.Domain.Models;
using Spotify.UI.Infrastructure.FileManager;
using System.Security.Claims;

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

        [HttpGet]
        public async Task<IActionResult> SetDefaultProfilePicture(
            string userId,
            [FromServices] SetDefaultProfilePicture setDefaultPicture,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemoveProfilePicture(getFileName.Execute(userId));

            await setDefaultPicture.Execute(userId);

            return RedirectToPage("/Accounts/UserProfile", new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> AddManager([FromServices] UserManager<ApplicationUser> userManager)
        {
            if (!(User.Identity as ClaimsIdentity).HasClaim("Role", "Manager"))
                await userManager.AddClaimAsync(await userManager.GetUserAsync(User), new Claim("Role", "Manager"));

            return RedirectToPage("/Admin/Index");
        }
    }
}
