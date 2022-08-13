using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.User;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LikeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private string GetUserId() => _userManager.GetUserId(HttpContext.User);

        [HttpPost("{id}")]
        public async Task<IActionResult> FollowMusician(int id, [FromServices] FollowMusician followMusician)
            => Ok(await followMusician.Execute(GetUserId(), id));

        [HttpPost("{id}")]
        public async Task<IActionResult> LikeAlbum(int id, [FromServices] LikeAlbum likeAlbum)
            => Ok(await likeAlbum.Execute(GetUserId(), id));

        [HttpPost("{id}")]
        public async Task<IActionResult> LikeSong(int id, [FromServices] LikeSong likeSong)
            => Ok(await likeSong.Execute(GetUserId(), id));
    }
}
