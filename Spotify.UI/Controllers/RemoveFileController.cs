﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;
using Spotify.Application.Admin.Musicians;
using Spotify.Application.Admin.Songs;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Policy = "Manager")]
    public class RemoveFileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

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
            [FromServices] GetAlbumFileName getAlbumFileName)
            => Ok(_fileManager.RemoveAlbumFile(getAlbumFileName.Execute(albumId)));

        [HttpDelete("{musicianId}")]
        public IActionResult Musician(
            int musicianId,
            [FromServices] GetMusicianFileName getMusicianFileName)
            => Ok(_fileManager.RemoveMusicianFile(getMusicianFileName.Execute(musicianId)));
    }
}
