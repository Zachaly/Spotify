using Microsoft.AspNetCore.Mvc;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    public class FileController : Controller
    {
        private IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("/Songs/{file}")]
        public IActionResult SongFile(string file)
            => new FileStreamResult(_fileManager.GetSongFile(file), "audio/mp3");

        [HttpGet("/Albums/{file}")]
        public IActionResult AlbumFile(string file)
            => new FileStreamResult(_fileManager.GetAlbumFile(file), "image/jpg");

        [HttpGet("/Musicians/{file}")]
        public IActionResult MusicianFile(string file)
            => new FileStreamResult(_fileManager.GetMusicianFile(file), "image/jpg");
    }
}
