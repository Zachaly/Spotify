using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;

namespace Spotify.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class AlbumController : Controller
    {
        [HttpGet("")]
        public IActionResult GetAlbums([FromServices] GetAlbums getAlbums)
            => Ok(getAlbums.Execute());

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
