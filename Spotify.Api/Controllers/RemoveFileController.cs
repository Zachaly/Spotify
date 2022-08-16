using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;
using Spotify.Application.Admin.Musicians;
using Spotify.Application.Admin.Songs;
using Spotify.Api.Infrastructure.FileManager;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize(Policy = "Manager")]
    public class RemoveFileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public RemoveFileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// Removes .mp3 file of given song
        /// </summary>
        [HttpDelete("{songId}")]
        public IActionResult Song(
            int songId,
            [FromServices] GetSongFileName getSongFileName) 
            => Ok(_fileManager.RemoveSongFile(getSongFileName.Execute(songId)));

        /// <summary>
        /// Removes cover picture of given album
        /// </summary>
        /// <param name="albumId"></param>
        [HttpDelete("{albumId}")]
        public IActionResult Album(
            int albumId,
            [FromServices] GetAlbumFileName getAlbumFileName)
            => Ok(_fileManager.RemoveAlbumFile(getAlbumFileName.Execute(albumId)));

        /// <summary>
        /// Removes picture of given musician
        /// </summary>
        [HttpDelete("{musicianId}")]
        public IActionResult Musician(
            int musicianId,
            [FromServices] GetMusicianFileName getMusicianFileName)
            => Ok(_fileManager.RemoveMusicianFile(getMusicianFileName.Execute(musicianId)));
    }
}
