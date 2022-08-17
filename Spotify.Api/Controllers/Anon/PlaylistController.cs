using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Playlists;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class PlaylistController : ControllerBase
    {
        /// <summary>
        /// Gets info about specific playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        /// <response code="200">
        /// Contains:
        /// * id - playlist id
        /// * name - playlist name
        /// * creatorId - id of creator
        /// * creatorName - name of creator
        /// * songs:
        ///     + id - song id
        ///     + name - song name
        ///     + creatorId - musician id
        ///     + creatorName - musician name
        ///     + albumId - album id
        ///     + albumName - album name
        /// </response>
        [HttpGet("{id}")]
        public IActionResult Playlist(int id, [FromServices] GetPlaylist getPlaylist)
            => Ok(getPlaylist.Execute(id));

        /// <summary>
        /// Gets list of all playlists of given user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <response code="200">
        /// List of playlists containing:
        /// * id - playlist id
        /// * name - playlist name
        /// * songCount - number of songs on playlist
        /// * creatorName - creator name
        /// </response>
        [HttpGet("/User/{userId}")]
        public IActionResult UserPlaylists(string userId, [FromServices] GetUserPlaylists getPlaylists)
            => Ok(getPlaylists.Execute(userId));
    }
}
