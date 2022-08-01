using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Songs;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Policy = "Admin")]
    public class RemoveFileController : Controller
    {
        private IFileManager _fileManager;

        public RemoveFileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpDelete("{songId}")]
        public IActionResult Song(
            int songId,
            [FromServices] GetSongFileName getSongFileName) 
            => Ok(_fileManager.RemoveSongFile(getSongFileName.Execute(songId)));

        [HttpDelete("{albumId}")]
        public IActionResult Album(
            int albumId,
            [FromServices] GetSongFileName getSongFileName)
            => Ok(_fileManager.RemoveSongFile(getSongFileName.Execute(albumId)));

        [HttpDelete("{musicianId}")]
        public IActionResult Musician(
            int musicianId,
            [FromServices] GetSongFileName getSongFileName)
            => Ok(_fileManager.RemoveSongFile(getSongFileName.Execute(musicianId)));
    }
}
