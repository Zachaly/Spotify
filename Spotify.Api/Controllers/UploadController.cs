using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            => Ok(await _fileManager.SaveSongFile(file));

        /// <summary>
        /// Uploads given .jpg file
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AlbumFile(IFormFile file)
            => Ok(await _fileManager.SaveAlbumFile(file));

        /// <summary>
        /// Uploads given .jpg file
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> MusicianFile(IFormFile file)
            => Ok(await _fileManager.SaveMusicianFile(file));
    }
}
