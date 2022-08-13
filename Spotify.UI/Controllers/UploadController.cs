using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Policy = "Manager")]
    public class UploadController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<IActionResult> SongFile(IFormFile file)
            => Ok(await _fileManager.SaveSongFile(file));

        public async Task<IActionResult> AlbumFile(IFormFile file)
            => Ok(await _fileManager.SaveAlbumFile(file));

        public async Task<IActionResult> MusicianFile(IFormFile file)
            => Ok(await _fileManager.SaveMusicianFile(file));
    }
}
