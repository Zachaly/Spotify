using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Musicians;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class MusicianController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMusicians([FromServices] GetMusicians getMusicians)
            => Ok(getMusicians.Execute());

        [HttpGet("{id}")]
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician)
            => Ok(getMusician.Execute(id));

        [HttpGet("Albums/{id}")]
        public IActionResult GetAlbums(int id, [FromServices] GetMusicianAlbums getAlbums)
            => Ok(getAlbums.Execute(id));
    }
}
