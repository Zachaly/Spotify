using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Songs;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers.Manager
{
    [Route("Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class SongController : ManagerController
    {
        public SongController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager) : base(userManager, appUserManager) { }

        [HttpGet("")]
        public IActionResult GetSongs([FromServices] GetManagerSongs getSongs) 
            => Ok(getSongs.Execute(GetId()));

        [HttpGet("{id}")]
        public IActionResult GetSong(int id, [FromServices] GetSong getSong) => Ok(getSong.Execute(id));

        [HttpPost("")]
        public async Task<IActionResult> AddSong(
            [FromBody] AddSong.Request request,
            [FromServices] AddSong addSong)
        {
            if (!IsManagerCorrect(request.CreatorId))
                return BadRequest();

            return Ok(await addSong.Execute(request));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateSong(
            [FromBody] UpdateSong.Request request,
            [FromServices] UpdateSong updateSong)
            => Ok(await updateSong.Execute(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id, [FromServices] DeleteSong deleteSong)
            => Ok(await deleteSong.Execute(id));
    }
}
