using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Albums;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class AlbumController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAlbums([FromServices] GetAlbums getAlbums)
            => Ok(getAlbums.Execute());

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id, [FromServices] GetAlbum getAlbum)
            => Ok(getAlbum.Execute(id));
    }
}
