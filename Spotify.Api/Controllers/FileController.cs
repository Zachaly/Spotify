using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.FileManager;
using Spotify.Application.Admin.Albums;
using Spotify.Application.Admin.Musicians;
using Spotify.Application.Admin.Songs;
using Spotify.Application.Playlists;
using Spotify.Application.User;

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
        /// <param name="id">Song id</param>
        [HttpGet("{id}")]
        public IActionResult Song(int id, [FromServices] GetSongFileName getFileName)
            => new FileStreamResult(_fileManager.GetSongFile(getFileName.Execute(id)), "audio/mp3");

        /// <summary>
        /// Returns .jpg file with album cover
        /// </summary>
        /// <param name="id">Song id</param>
        [HttpGet("{id}")]
        public IActionResult Album(int id, [FromServices] GetAlbumFileName getFileName)
            => new FileStreamResult(_fileManager.GetAlbumFile(getFileName.Execute(id)), "image/jpg");

        /// <summary>
        /// Returns .jpg file with picture of musician
        /// </summary>
        /// <param name="id">Musician id</param>
        [HttpGet("{id}")]
        public IActionResult Musician(int id, [FromServices] GetMusicianFileName getFileName)
            => new FileStreamResult(_fileManager.GetMusicianFile(getFileName.Execute(id)), "image/jpg");

        /// <summary>
        /// Returns .jpg file with profile picture
        /// </summary>
        /// <param name="id">User id</param>
        [HttpGet("{id}")]
        public IActionResult Profile(string id, [FromServices] GetProfilePictureFileName getFileName)
            => new FileStreamResult(_fileManager.GetProfilePicture(getFileName.Execute(id)), "image/jpg");

        /// <summary>
        /// Returns .jpg file with playlist cover
        /// </summary>
        /// <param name="id">Playlist id</param>
        [HttpGet("{id}")]
        public IActionResult Playlist(int id, [FromServices] GetPlaylistFileName getFileName)
            => new FileStreamResult(_fileManager.GetPlaylistPicture(getFileName.Execute(id)), "image/jpg");
    }
}
