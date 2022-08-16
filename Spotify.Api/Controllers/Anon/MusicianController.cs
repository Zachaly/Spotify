using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Musicians;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class MusicianController : ControllerBase
    {
        /// <summary>
        /// Get list of all musicians
        /// </summary>
        /// <response code="200">
        /// List of musicians containing:
        /// * id - musician id
        /// * name - musician name
        /// * fileName - name of file containing musician picture
        /// * plays - total number of musician's songs plays
        /// </response>
        [HttpGet]
        public IActionResult GetMusicians([FromServices] GetMusicians getMusicians)
            => Ok(getMusicians.Execute());

        /// <summary>
        /// Gets specific musician info
        /// </summary>
        /// <param name="id">Musician id</param>
        /// <response code="200">
        /// Contains:
        /// * id - musician id
        /// * name - musician name
        /// * description - musician's description
        /// * numberOfFollowers - number of followers
        /// * fileName - name of file containing picture of musician
        /// * topAlbums(5 most played albums):
        ///     + id - album id
        ///     + name - album name
        ///     + plays - total number of song plays of the album
        ///     + fileName - album cover file name
        /// * topSongs(10 most played songs):
        ///     + id - song id
        ///     + name - song name
        ///     + albumId - id of album that contains this song
        ///     + plays - total number of times song was played
        ///     + fileName - song mp3 file name
        ///     + albumFileName - name file containing album cover
        /// </response>
        [HttpGet("{id}")]
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician)
            => Ok(getMusician.Execute(id));

        /// <summary>
        /// Gets albums of given musician
        /// </summary>
        /// <param name="id">Musician id</param>
        /// <response code="200">
        /// List of albums containg: 
        ///     + id - album id
        ///     + name - album name
        ///     + creatorName - name of musician
        ///     + fileName - album cover file name
        /// </response>
        [HttpGet("Albums/{id}")]
        public IActionResult GetAlbums(int id, [FromServices] GetMusicianAlbums getAlbums)
            => Ok(getAlbums.Execute(id));
    }
}
