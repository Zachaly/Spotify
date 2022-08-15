using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.FileManager;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize(Policy = "Manager")]
    public class UploadController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        public async Task<IActionResult> SongFile(IFormFile file)
            => Ok(await _fileManager.SaveSongFile(file));

        [HttpPost]
        public async Task<IActionResult> AlbumFile(IFormFile file)
            => Ok(await _fileManager.SaveAlbumFile(file));

        [HttpPost]
        public async Task<IActionResult> MusicianFile(IFormFile file)
            => Ok(await _fileManager.SaveMusicianFile(file));
    }
}
