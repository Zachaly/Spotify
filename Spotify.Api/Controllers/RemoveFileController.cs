using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;
using Spotify.Application.Admin.Musicians;
using Spotify.Application.Admin.Songs;
using Spotify.Api.Infrastructure.FileManager;
using Spotify.Api.Infrastructure.AuthManager;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize(Policy = "Manager")]
    public class RemoveFileController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly IAuthManager _authManager;

        public RemoveFileController(IFileManager fileManager, IAuthManager authManager)
        {
            _fileManager = fileManager;
            _authManager = authManager;
        }

        /// <summary>
        /// Removes .mp3 file of given song
        /// </summary>
        [HttpDelete("{songId}")]
        public IActionResult Song(
            int songId,
            [FromServices] GetSongFileName getSongFileName)
        { 
            if(_authManager.IsSongManagerCorrect(_authManager.GetCurrentUserId(), songId))
                return BadRequest();

            return Ok(_fileManager.RemoveSongFile(getSongFileName.Execute(songId)));
        }

        /// <summary>
        /// Removes cover picture of given album
        /// </summary>
        [HttpDelete("{albumId}")]
        public IActionResult Album(
            int albumId,
            [FromServices] GetAlbumFileName getAlbumFileName)
        {
            if (!_authManager.IsAlbumManagerCorrect(_authManager.GetCurrentUserId(), albumId))
                return BadRequest();

            return Ok(_fileManager.RemoveAlbumFile(getAlbumFileName.Execute(albumId)));
        }

        /// <summary>
        /// Removes picture of given musician
        /// </summary>
        [HttpDelete("{musicianId}")]
        public IActionResult Musician(
            int musicianId,
            [FromServices] GetMusicianFileName getMusicianFileName)
        {
            if (!_authManager.IsMusicianManagerCorrect(_authManager.GetCurrentUserId(), musicianId))
                return BadRequest();

            return Ok(_fileManager.RemoveMusicianFile(getMusicianFileName.Execute(musicianId)));
        }
    }
}
