using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.User;
using Spotify.Domain.Models;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeController(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId() => _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);

        /// <summary>
        /// Creates or deletes follow of given musician by currently logged user
        /// </summary>
        /// <param name="id">Id of musician</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> FollowMusician(int id, [FromServices] FollowMusician followMusician)
            => Ok(await followMusician.Execute(GetUserId(), id));

        /// <summary>
        /// Creates or deletes like of given album by currently logged user
        /// </summary>
        /// <param name="id">Id of album</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> LikeAlbum(int id, [FromServices] LikeAlbum likeAlbum)
            => Ok(await likeAlbum.Execute(GetUserId(), id));

        /// <summary>
        /// Creates or deletes like of given song by currently logged user
        /// </summary>
        /// <param name="id">Id of song</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> LikeSong(int id, [FromServices] LikeSong likeSong)
            => Ok(await likeSong.Execute(GetUserId(), id));
    }
}
