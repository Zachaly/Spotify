using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.AuthManager;
using Spotify.Application.Admin.Musicians;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers.Manager
{
    [Route("/api/Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class MusicianController : ControllerBase
    {
        private IAuthManager _authManager;

        public MusicianController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /// <summary>
        /// Get list of all musicians, filtered by currently logged user id
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
        public IActionResult GetMusicians([FromServices] GetManagerMusicians getMusicians) 
            => Ok(getMusicians.Execute(_authManager.GetCurrentUserId()));

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
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician)
        {
            if (_authManager.IsMusicianManagerCorrect(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            return Ok(getMusician.Execute(id));
        }

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
        {
            request.ManagerId = _authManager.GetCurrentUserId();

            return Ok(await addMusician.Execute(request));
        }

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
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpPut("")]
        public async Task<IActionResult> UpdateMusician(
            [FromBody] UpdateMusician.Request request,
            [FromServices] UpdateMusician updateMusician)
        {
            if (_authManager.IsMusicianManagerCorrect(_authManager.GetCurrentUserId(), request.Id))
                return BadRequest();

            return Ok(await updateMusician.Execute(request));
        }


        /// <summary>
        /// Removes musician from database
        /// </summary>
        /// <param name="id">Musician id</param>
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id, [FromServices] DeleteMusician deleteMusician)
        {
            if (_authManager.IsMusicianManagerCorrect(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            return Ok(await deleteMusician.Execute(id));
        }
    }
}
