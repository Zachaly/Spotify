using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.AuthManager;
using Spotify.Application.User;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public LikeController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        /// <summary>
        /// Creates or deletes follow of given musician by currently logged user
        /// </summary>
        /// <param name="id">Id of musician</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> FollowMusician(int id, [FromServices] FollowMusician followMusician)
            => Ok(await followMusician.Execute(_authManager.GetCurrentUserId(), id));

        /// <summary>
        /// Creates or deletes like of given album by currently logged user
        /// </summary>
        /// <param name="id">Id of album</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> LikeAlbum(int id, [FromServices] LikeAlbum likeAlbum)
            => Ok(await likeAlbum.Execute(_authManager.GetCurrentUserId(), id));

        /// <summary>
        /// Creates or deletes like of given song by currently logged user
        /// </summary>
        /// <param name="id">Id of song</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> LikeSong(int id, [FromServices] LikeSong likeSong)
            => Ok(await likeSong.Execute(_authManager.GetCurrentUserId(), id));
    }
}
