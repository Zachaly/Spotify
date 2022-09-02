using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Albums;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class AlbumController : ControllerBase
    {
        /// <summary>
        /// Gets all albums from database without needing authorization
        /// </summary>
        /// <response code="200">
        /// List of albums containing:
        /// * id - album id
        /// * name - album name
        /// * creatorName - creator name
        /// * plays - combined number of plays of all songs on the album
        /// </response>
        [HttpGet]
        public IActionResult GetAlbums([FromServices] GetAlbums getAlbums)
            => Ok(getAlbums.Execute());

        /// <summary>
        /// Gets specific album from database
        /// </summary>
        /// <param name="id">Album id</param>
        /// <response code="200">
        /// Constains:
        /// * id - album id
        /// * name - album name
        /// * songCount - number of songs
        /// * songs:
        ///     + id - song id
        ///     + creatorId - creator id
        ///     + creatorName - creator name
        ///     + name - song name
        ///     + albumId - id of album
        ///     + plays - total number of times when song was played
        /// </response>
        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id, [FromServices] GetAlbum getAlbum)
            => Ok(getAlbum.Execute(id));
    }
}
