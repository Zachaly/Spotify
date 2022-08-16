using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Musicians;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers.Admin
{
    [Route("/api/Admin/[controller]")]
    [Authorize(Policy = "Admin")]
    public class MusicianController : ControllerBase
    {
        /// <summary>
        /// Get list of all musicians
        /// </summary>
        /// <response code="200">
        /// List of musicians containing:
        /// * id - musician id
        /// * name - musician name
        /// * numberOfSongs - number of songs
        /// * numberOfFollowers - number of followers
        /// * numberOfPlays - total number of musician's songs plays
        /// </response>
        [HttpGet("")]
        public IActionResult GetMusicians([FromServices] GetMusicians getMusicians) => Ok(getMusicians.Execute());

        /// <summary>
        /// Gets specific musician info
        /// </summary>
        /// <param name="id">Musician id</param>
        /// <response code="200">
        /// Contains:
        /// * id - musician id
        /// * name - musician name
        /// * description - musician's description
        /// * numberOfPlays - total number of musician's songs plays
        /// * numberOfFollowers - number of followers
        /// * albums:
        ///     + id - album id
        ///     + name - album name
        ///     + songs:
        ///         - id - song id
        ///         - name - song name
        ///         - plays - number of times song was played
        /// </response>
        [HttpGet("{id}")]
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician) => Ok(getMusician.Execute(id));

        /// <summary>
        /// Adds musician
        /// </summary>
        /// <param name="request">
        /// Constists of:
        /// * name - musician's name
        /// * description - musician's description
        /// * fileName - name of file with musician's pitcture
        /// </param>
        /// <returns>
        /// Newly added musician:
        /// * id - musician id
        /// * name - musician name
        /// * numberOfSongs - number of songs
        /// * numberOfFollowers - number of followers
        /// * numberOfPlays - total number of musician's songs plays
        /// </returns>
        [HttpPost("")]
        public async Task<IActionResult> AddMusician(
            [FromBody] AddMusician.Request request,
            [FromServices] AddMusician addMusician)
            => Ok(await addMusician.Execute(request));

        /// <summary>
        /// Updates musician with info given in request
        /// </summary>
        /// <param name="request">
        /// Constists of:
        /// * id - musician's id
        /// * name - new name
        /// * description - new description
        /// </param>
        /// <response code="200">
        /// Musician with updated info:
        /// * id - musician id
        /// * name - musician name
        /// * numberOfSongs - number of songs
        /// * numberOfFollowers - number of followers
        /// * numberOfPlays - total number of musician's songs plays
        /// </response>
        [HttpPut("")]
        public async Task<IActionResult> UpdateMusician(
            [FromBody] UpdateMusician.Request request,
            [FromServices] UpdateMusician updateMusician)
            => Ok(await updateMusician.Execute(request));

        /// <summary>
        /// Removes musician from database
        /// </summary>
        /// <param name="id">Musician id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id, [FromServices] DeleteMusician deleteMusician)
            => Ok(await deleteMusician.Execute(id));
    }
}
