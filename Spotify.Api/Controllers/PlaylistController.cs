using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Playlists;
using Spotify.Api.Infrastructure.FileManager;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class PlaylistController : ControllerBase
    {
        /// <summary>
        /// Adds song to playlist
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="playlistId"></param>
        [HttpPost("{playlistId}/{songId}")]
        public async Task<IActionResult> AddSong(int songId, int playlistId,
            [FromServices] AddSong addSong)
            => Ok(await addSong.Execute(songId, playlistId));

        /// <summary>
        /// Removes song from playlist
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="playlistId"></param>
        [HttpDelete("{playlistId}/{songId}")]
        public async Task<IActionResult> RemoveSong(int songId, int playlistId,
            [FromServices] RemoveSong removeSong)
            => Ok(await removeSong.Execute(songId, playlistId));

        /// <summary>
        /// Removes given playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        [HttpGet("id")]
        public async Task<IActionResult> RemovePlaylist(int id,
            [FromServices] RemovePlaylist removePlaylist,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemovePlaylistPicture(getFileName.Execute(id));
            await removePlaylist.Execute(id);
            return Ok();
        }

        /// <summary>
        /// Sets default cover picture for playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        [HttpGet]
        public async Task<IActionResult> SetDefaultCover(int id,
            [FromServices] SetCoverPicture setCoverPicture,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemovePlaylistPicture(getFileName.Execute(id));

            await setCoverPicture.Execute(id, "placeholder.jpg");

            return Ok();
        }
    }
}
