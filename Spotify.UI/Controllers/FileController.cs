using Microsoft.AspNetCore.Mvc;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    public class FileController : Controller
    {
        [HttpGet("/Songs/{file}")]
        public IActionResult SongFile(string file, [FromServices] IFileManager fileManager)
            => new FileStreamResult(fileManager.GetSongFile(file), "audio/mp3");
    }
}
