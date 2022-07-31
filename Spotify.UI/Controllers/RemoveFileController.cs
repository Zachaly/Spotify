using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Songs;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Policy = "Admin")]
    public class RemoveFileController : Controller
    {
        [HttpDelete("{songId}")]
        public IActionResult Song(
            int songId,
            [FromServices] IFileManager fileManager,
            [FromServices] GetSongFileName getSongFileName) 
            => Ok(fileManager.RemoveSongFile(getSongFileName.Execute(songId)));
    }
}
