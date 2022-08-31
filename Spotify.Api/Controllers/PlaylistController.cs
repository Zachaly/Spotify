using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Playlists;
using Spotify.Api.Infrastructure.FileManager;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Spotify.Api.Infrastructure.AuthManager;
using Spotify.Api.DTO;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class PlaylistController : ControllerBase
    {
        private IAuthManager _authManager;

        public PlaylistController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /// <summary>
        /// Adds song to playlist
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="playlistId"></param>
        [HttpPost("{playlistId}/{songId}")]
        [Authorize]
        public async Task<IActionResult> AddSong(int songId, int playlistId,
            [FromServices] AddSong addSong)
        {
            if (!_authManager.IsPlaylistCreator(_authManager.GetCurrentUserId(), playlistId))
                return BadRequest();

            return Ok(await addSong.Execute(songId, playlistId));
        }

        /// <summary>
        /// Removes song from playlist
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="playlistId"></param>
        [HttpDelete("{playlistId}/{songId}")]
        [Authorize]
        public async Task<IActionResult> RemoveSong(int songId, int playlistId,
            [FromServices] RemoveSong removeSong)
        {
            if (!_authManager.IsPlaylistCreator(_authManager.GetCurrentUserId(), playlistId))
                return BadRequest();

            return Ok(await removeSong.Execute(songId, playlistId));
        }

        /// <summary>
        /// Removes given playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemovePlaylist(int id,
            [FromServices] RemovePlaylist removePlaylist,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            if (!_authManager.IsPlaylistCreator(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            fileManager.RemovePlaylistPicture(getFileName.Execute(id));
            await removePlaylist.Execute(id);
            return Ok();
        }

        /// <summary>
        /// Sets default cover picture for playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetDefaultCover(int id,
            [FromServices] SetCoverPicture setCoverPicture,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            if (!_authManager.IsPlaylistCreator(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            fileManager.RemovePlaylistPicture(getFileName.Execute(id));

            await setCoverPicture.Execute(id, "placeholder.jpg");

            return Ok();
        }

        /// <summary>
        /// Adds playlist
        /// </summary>
        /// <param name="playlistModel">
        /// Must be send as formdata with 'Content-type': 'multipart/form-data' header/
        /// Consists of: 
        /// * name - playlist title
        /// * coverPicture - cover picture
        /// </param>
        /// <returns>
        /// Playlist model:
        /// * id - playlist id
        /// * name - playlist name
        /// * songCount - number of songs on playlist
        /// * creatorName - creator name
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPlaylist(
            [FromForm] PlaylistModel playlistModel,
            [FromServices] AddPlaylist addPlaylist,
            [FromServices] IFileManager fileManager,
            [FromServices] IAuthManager authManager)
        {
            string fileName = "placeholder.jpg";

            if(playlistModel.CoverPicture != null)
            {
                fileName = await fileManager.SavePlaylistPicture(playlistModel.CoverPicture);
            }

            var result = await addPlaylist.Execute(new AddPlaylist.Request
            {
                FileName = fileName,
                Name = playlistModel.Name,
                UserId = authManager.GetCurrentUserId()
            });

            return Ok(result);
        }
    }
}
