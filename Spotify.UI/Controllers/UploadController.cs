using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Policy = "Admin")]
    public class UploadController : Controller
    {
        public async Task<IActionResult> SongFile(IFormFile file, [FromServices] IFileManager fileManager)
            => Ok(await fileManager.SaveSongFile(file));
    }
}
