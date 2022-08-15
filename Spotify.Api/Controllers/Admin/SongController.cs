using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Songs;

namespace Spotify.Api.Controllers.Admin
{
    [Route("/api/Admin/[controller]")]
    [Authorize(Policy = "Admin")]
    public class SongController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetSongs([FromServices] GetSongs getSongs) => Ok(getSongs.Execute());

        [HttpGet("{id}")]
        public IActionResult GetSong(int id, [FromServices] GetSong getSong) => Ok(getSong.Execute(id));

        [HttpPost("")]
        public async Task<IActionResult> AddSong(
            [FromBody] AddSong.Request request,
            [FromServices] AddSong addSong)
            => Ok(await addSong.Execute(request));

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
