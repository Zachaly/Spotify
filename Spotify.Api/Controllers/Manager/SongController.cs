using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Api.Infrastructure.AuthManager;
using Spotify.Application.Admin.Songs;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers.Manager
{
    [Route("/api/Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class SongController : ControllerBase
    {
        private IAuthManager _authManager;

        public SongController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /// <summary>
        /// Gets list of all songs grouped by album, filtered by current user id
        /// </summary>
        /// <response code="200">
        /// List of albums containing:
        /// * id - album id
        /// * name - album name
        /// * creatorId - album creator's id
        /// * creatorName - album creator's name
        /// * songs:
        ///     + id - song id
        ///     + name - song name
        ///     + plays - total number of plays
        /// </response>
        [HttpGet("")]
        public IActionResult GetSongs([FromServices] GetManagerSongs getSongs)
            => Ok(getSongs.Execute(_authManager.GetCurrentUserId()));

        /// <summary>
        /// Gets info about specific song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <response code="200">
        /// Contains:
        /// * id - song id
        /// * name - song name
        /// * plays - total number of plays
        /// * creatorId - creator id
        /// * creatorName - creator name
        /// * albumId - album id
        /// * albumName - album name
        /// </response>
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpGet("{id}")]
        public IActionResult GetSong(int id, [FromServices] GetSong getSong)
        {
            if (_authManager.IsSongManagerCorrect(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            return Ok(getSong.Execute(id));
        }


        /// <summary>
        /// Adds a new song
        /// </summary>
        /// <param name="request">
        /// Consists of:
        /// * name - song name
        /// * creatorId - id of musician
        /// * fileName - name of mp3 file
        /// * albumId - id of album containing the song
        /// </param>
        /// <response code="200">
        /// Newly added song: 
        ///     + id - song id
        ///     + name - song name
        ///     + plays - total number of plays
        /// </response>
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpPost("")]
        public async Task<IActionResult> AddSong(
            [FromBody] AddSong.Request request,
            [FromServices] AddSong addSong)
        {
            if (_authManager.IsMusicianManagerCorrect(_authManager.GetCurrentUserId(), request.CreatorId))
                return BadRequest();

            return Ok(await addSong.Execute(request));
        }

        /// <summary>
        /// Updates song with info given in request
        /// </summary>
        /// <param name="request">
        /// Consists of: 
        /// * id - song id
        /// * name - song name
        /// </param>
        /// <response code="200">
        /// Updated song: 
        ///     + id - song id
        ///     + name - song name
        ///     + plays - total number of plays
        /// </response>
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpPut("")]
        public async Task<IActionResult> UpdateSong(
            [FromBody] UpdateSong.Request request,
            [FromServices] UpdateSong updateSong)
        {
            if (_authManager.IsSongManagerCorrect(_authManager.GetCurrentUserId(), request.Id))
                return BadRequest();

            return Ok(await updateSong.Execute(request));
        }

        /// <summary>
        /// Removes song from database
        /// </summary>
        /// <param name="id">song id</param>
        /// <response code="400">
        /// Current user is not manager of given musician
        /// </response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id, [FromServices] DeleteSong deleteSong)
        {
            if (_authManager.IsSongManagerCorrect(_authManager.GetCurrentUserId(), id))
                return BadRequest();

            return Ok(await deleteSong.Execute(id)); 
        }
    }
}
