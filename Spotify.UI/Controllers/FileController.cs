using Microsoft.AspNetCore.Mvc;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    public class FileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

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

        [HttpGet("/ProfilePicture/{file}")]
        public IActionResult ProfilePicture(string file)
            => new FileStreamResult(_fileManager.GetProfilePicture(file), "image/jpg");

        [HttpGet("/Playlists/{file}")]
        public IActionResult PlaylistCover(string file)
            => new FileStreamResult(_fileManager.GetPlaylistPicture(file), "image/jpg");
    }
}
