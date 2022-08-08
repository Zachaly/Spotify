using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers.Manager
{
    [Route("Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class AlbumController : ManagerController
    {
        public AlbumController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager) : base(userManager, appUserManager)
        {
        }

        [HttpGet("")]
        public IActionResult GetAlbums([FromServices] GetManagerAlbums getAlbums)
            => Ok(getAlbums.Execute(GetId()));

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id, [FromServices] GetAlbum getAlbum)
            => Ok(getAlbum.Execute(id));

        [HttpPost("")]
        public async Task<IActionResult> AddAlbum(
            [FromBody] AddAlbum.Request request,
            [FromServices] AddAlbum addAlbum)
            => Ok(await addAlbum.Execute(request));

        [HttpPut("")]
        public async Task<IActionResult> UpdateAlbum(
            [FromBody] UpdateAlbum.Request request,
            [FromServices] UpdateAlbum updateAlbum)
            => Ok(await updateAlbum.Execute(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id, [FromServices] DeleteAlbum deleteAlbum)
            => Ok(await deleteAlbum.Execute(id));
    }
}
