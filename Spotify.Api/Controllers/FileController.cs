using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.FileManager;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// Returns .mp3 file
        /// </summary>
        /// <param name="file">Name of requested file</param>
        [HttpGet("{file}")]
        public IActionResult Song(string file)
            => new FileStreamResult(_fileManager.GetSongFile(file), "audio/mp3");

        /// <summary>
        /// Returns .jpg file with album cover
        /// </summary>
        /// <param name="file">Name of requested file</param>
        [HttpGet("{file}")]
        public IActionResult Album(string file)
            => new FileStreamResult(_fileManager.GetAlbumFile(file), "image/jpg");

        /// <summary>
        /// Returns .jpg file with picture of musician
        /// </summary>
        /// <param name="file">Name of requested file</param>
        [HttpGet("{file}")]
        public IActionResult Musician(string file)
            => new FileStreamResult(_fileManager.GetMusicianFile(file), "image/jpg");

        /// <summary>
        /// Returns .jpg file with profile picture
        /// </summary>
        /// <param name="file">Name of requested file</param>
        [HttpGet("{file}")]
        public IActionResult Profile(string file)
            => new FileStreamResult(_fileManager.GetProfilePicture(file), "image/jpg");

        /// <summary>
        /// Returns .jpg file with playlist cover
        /// </summary>
        /// <param name="file">Name of requested file</param>
        [HttpGet("{file}")]
        public IActionResult Playlist(string file)
            => new FileStreamResult(_fileManager.GetPlaylistPicture(file), "image/jpg");
    }
}
