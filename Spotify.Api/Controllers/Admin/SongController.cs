using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Songs;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers.Admin
{
    [Route("/api/Admin/[controller]")]
    [Authorize(Policy = "Admin")]
    public class SongController : ControllerBase
    {
        /// <summary>
        /// Gets list of all songs grouped by album
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
        public IActionResult GetSongs([FromServices] GetSongs getSongs) => Ok(getSongs.Execute());

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
        [HttpGet("{id}")]
        public IActionResult GetSong(int id, [FromServices] GetSong getSong) => Ok(getSong.Execute(id));

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
        [HttpPost("")]
        public async Task<IActionResult> AddSong(
            [FromBody] AddSong.Request request,
            [FromServices] AddSong addSong)
            => Ok(await addSong.Execute(request));

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
        [HttpPut("")]
        public async Task<IActionResult> UpdateSong(
            [FromBody] UpdateSong.Request request,
            [FromServices] UpdateSong updateSong)
            => Ok(await updateSong.Execute(request));

        /// <summary>
        /// Removes song from database
        /// </summary>
        /// <param name="id">song id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id, [FromServices] DeleteSong deleteSong)
            => Ok(await deleteSong.Execute(id));
    }
}
