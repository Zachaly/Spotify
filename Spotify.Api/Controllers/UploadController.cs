using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure;
using Spotify.Api.Infrastructure.FileManager;
using System.Threading.Tasks;

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

        /// <summary>
        /// Uploads given .mp3 file
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SongFile(IFormFile file)
        {
            if (file.IsExtensionCorrect(".mp3"))
            {
                return Ok(await _fileManager.SaveSongFile(file));
            }

            return BadRequest(ExtentionErrorMessage(".mp3"));
        }

        /// <summary>
        /// Uploads given .jpg file
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AlbumFile(IFormFile file)
        {
            if (file.IsExtensionCorrect(".jpg"))
            {
                return Ok(await _fileManager.SaveAlbumFile(file));
            }

            return BadRequest(ExtentionErrorMessage(".jpg"));
        }

        /// <summary>
        /// Uploads given .jpg file
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> MusicianFile(IFormFile file)
        {
            if (file.IsExtensionCorrect(".jpg"))
            {
                return Ok(await _fileManager.SaveMusicianFile(file));
            }

            return BadRequest(ExtentionErrorMessage(".jpg"));
        }

        private string ExtentionErrorMessage(string expectedExtention)
            => $"Invalid file extention! Expected {expectedExtention}";

        /// <summary>
        /// Gets placeholder filename
        /// </summary>
        [HttpGet]
        public IActionResult Placeholder() => Ok("placeholder.jpg");
    }
}
