using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Playlists;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class PlaylistController : Controller
    {
        [HttpPost("{playlistId}/{songId}")]
        public async Task<IActionResult> AddSong(int songId, int playlistId,
            [FromServices] AddSong addSong)
            => Ok(await addSong.Execute(songId, playlistId));

        [HttpDelete("{playlistId}/{songId}")]
        public async Task<IActionResult> RemoveSong(int songId, int playlistId,
            [FromServices] RemoveSong removeSong)
            => Ok(await removeSong.Execute(songId, playlistId));

        [HttpGet]
        public async Task<IActionResult> RemovePlaylist(int id,
            [FromServices] RemovePlaylist removePlaylist,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemovePlaylistPicture(getFileName.Execute(id));
            await removePlaylist.Execute(id);
            return RedirectToPage("/Accounts/UserProfile");
        }

        [HttpGet]
        public async Task<IActionResult> SetDefaultCover(int id,
            [FromServices] SetCoverPicture setCoverPicture,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemovePlaylistPicture(getFileName.Execute(id));

            await setCoverPicture.Execute(id, "placeholder.jpg");

            return RedirectToPage("/PlaylistPage", new { id = id });
        }
    }
}
