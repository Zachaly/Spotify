using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Albums;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System.Threading.Tasks;

namespace Spotify.Api.Controllers.Manager
{
    [Route("/api/Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class AlbumController : ManagerController
    {
        public AlbumController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager,
            IHttpContextAccessor httpContextAccessor) 
            : base(userManager, appUserManager, httpContextAccessor) { }

        /// <summary>
        /// Get all albums in database grouped by musicians, filtered by id of currently logged user
        /// </summary>
        /// <response code="200">
        /// List of musicians containing: 
        /// * name - musician name 
        /// * id - musician id 
        /// * albums: 
        ///     + name - album name 
        ///     + id - album id 
        ///     + songCount - number of songs on the album
        /// </response>
        [HttpGet("")]
        public IActionResult GetAlbums([FromServices] GetManagerAlbums getAlbums)
            => Ok(getAlbums.Execute(GetId()));

        /// <summary>
        /// Get specific info for given album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <response code="200">
        /// Object containing: 
        /// * id - album id 
        /// * name - album name 
        /// * creatorId - album creator id 
        /// * creator - creator name 
        /// * songs: 
        ///     + id - song id 
        ///     + name - song name 
        ///     + numberOfPlays - number of times when song was played
        /// </response>
        /// <response code="400">
        /// User is not allowed to manage this album
        /// </response>
        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id, [FromServices] GetAlbum getAlbum)
        {
            if (!IsManagerCorrect(id))
                return BadRequest();

            return Ok(getAlbum.Execute(id)); 
        }

        /// <summary>
        /// Add album with data given in request
        /// </summary>
        /// <param name="request">
        /// Consists of: 
        /// * name - album name 
        /// * musicianId - creator id 
        /// * fileName - name of file on server containing cover picture of album 
        /// </param>
        /// <response code="200">
        /// New album object containing: 
        /// * id - album id 
        /// * name - album name 
        /// * creator - album creator name 
        /// * songCount - album song count (0)
        /// </response>
        /// <response code="400">
        /// User is not allowed to manage albums of musician given in request
        /// </response>
        [HttpPost("")]
        public async Task<IActionResult> AddAlbum(
            [FromBody] AddAlbum.Request request,
            [FromServices] AddAlbum addAlbum)
        {
            if (!IsManagerCorrect(request.MusicianId))
                return BadRequest();

            return Ok(await addAlbum.Execute(request));
        }

        /// <summary>
        /// Updates album with info given in request
        /// </summary>
        /// <param name="request">
        /// Consists of: 
        /// * id - album id 
        /// * name - new name of the album
        /// </param>
        /// <response code="200">
        /// New album object containing: 
        /// * id - album id 
        /// * name - album name 
        /// * creator - album creator name 
        /// * songCount - album song count (0)
        /// </response>
        [HttpPut("")]
        public async Task<IActionResult> UpdateAlbum(
            [FromBody] UpdateAlbum.Request request,
            [FromServices] UpdateAlbum updateAlbum)
            => Ok(await updateAlbum.Execute(request));
        /// <summary>
        /// Removes album from database
        /// </summary>
        /// <param name="id">
        /// Id of deleted album
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id, [FromServices] DeleteAlbum deleteAlbum)
            => Ok(await deleteAlbum.Execute(id));
    }
}
