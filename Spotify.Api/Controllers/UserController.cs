using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Songs;
using Spotify.Application.User;
using Spotify.Domain.Models;
using Spotify.Api.Infrastructure.FileManager;
using System.Security.Claims;
using Spotify.Api.DTO;
using Spotify.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using Spotify.Api.Infrastructure.AuthManager;
using Microsoft.AspNetCore.Http;
using Spotify.Domain.Infrastructure;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private IAuthManager _authManager;

        public UserController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        /// <summary>
        /// Adds a play to given song
        /// </summary>
        /// <param name="id">Id of the song</param>
        [HttpPost("{id}")]
        public async Task<IActionResult> AddPlay(int id, [FromServices] AddPlay addPlay)
            => Ok(await addPlay.Execute(id));

        /// <summary>
        /// Sets default profile picture for current user
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SetDefaultProfilePicture(
            [FromServices] SetDefaultProfilePicture setDefaultPicture,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            var userId = _authManager.GetCurrentUserId();

            fileManager.RemoveProfilePicture(getFileName.Execute(userId));

            await setDefaultPicture.Execute(userId);

            return Ok();
        }

        /// <summary>
        /// Makes current user a manager
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddManager(
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext.User;

            if (!user.HasClaim("Role", "Manager") && !user.HasClaim("Role", "Admin"))
                await userManager.AddClaimAsync(await userManager.GetUserAsync(User), new Claim("Role", "Manager"));

            return Ok();
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="registerModel">
        /// Contains:
        /// * username - user name
        /// * password - user password
        /// * email - user email
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Register(
            [FromBody] RegisterModel registerModel,
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] RegisterModelValidator validator)
        {
            var result = validator.Validate(registerModel);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage);

                return BadRequest(errors);
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                FileName = "placeholder.jpg"
            };

            await userManager.CreateAsync(user, registerModel.Password);

            return Ok();
        }

        /// <summary>
        /// Authenticates user with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <response code="200">
        /// Returns JWT token marked as auth_token
        /// </response>
        [HttpPost("{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password,
            [FromServices] UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                return BadRequest("Username or password is incorrect");

            var result = await userManager.CheckPasswordAsync(user, password);

            if (!result)
                return BadRequest("Username or password is incorrect");

            var token = await _authManager.GetToken(user);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { auth_token = tokenJson});
        }

        /// <summary>
        /// Gets profile info
        /// </summary>
        /// <param name="id">User id</param>
        /// <response code="200">
        /// Contains:
        /// * name - username
        /// * id - user id
        /// * likedSongsCount - number of song likes of given user
        /// * followedMusicians:
        ///     + id - musician id
        ///     + name - musician name
        /// * likedAlbums:
        ///     + id - album id
        ///     + name - album name
        ///     + creatorName - musician name
        /// </response>
        [HttpGet("{id}")]
        public IActionResult Profile(string id, [FromServices] GetUserProfile getUser)
            => Ok(getUser.Execute(id));

        /// <summary>
        /// Gets likes song of given user
        /// </summary>
        /// <param name="id"> User id</param>
        /// <response code="200">
        /// List of songs containing:
        /// * id - song id
        /// * name - song name
        /// * creatorId - musician's id
        /// * creatorName - musician's name
        /// * albumId - album id
        /// * albumName - album name
        /// </response>
        [HttpGet("{id}")]
        public IActionResult UserLikedSongs(string id, [FromServices] GetUserLikedSongs getSongs)
            => Ok(getSongs.Execute(id));

        /// <summary>
        /// Updates info about current user
        /// </summary>
        /// <param name="request">
        /// Consists of:
        /// * userName - new username of user
        /// * fileName - name of new profile picture (set null or empty if not changed)
        /// </param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateProfileInfoModel request,
            [FromServices] UpdateUser updateUser,
            [FromServices] IFileManager fileManager,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] UpdateProfileInfoValidator validator)
        {
            var userId = _authManager.GetCurrentUserId();

            var result = validator.Validate(request);

            if (!result.IsValid || string.IsNullOrEmpty(userId))
            {
                var errors = result.Errors.Select(error => error.ErrorMessage);
                return BadRequest(errors);
            }

            var currentPicture = getFileName.Execute(userId);

            if (request.ProfilePicture != null && currentPicture != "placeholder.jpg")
                fileManager.RemoveProfilePicture(currentPicture);

            await updateUser.Execute(new UpdateUser.Request
            {
                FileName = await fileManager.SaveProfilePicture(request.ProfilePicture),
                UserName = request.Username,
                Id = userId
            });

            return Ok();
        }

        /// <summary>
        /// Returns highest claim that current user has
        /// </summary>
        [HttpGet]
        [Authorize]
        public IActionResult Claim()
            => Ok(_authManager.GetHighestUserClaim(_authManager.GetCurrentUserId()));

        /// <summary>
        /// Gets id of current user
        /// </summary>
        [HttpGet]
        [Authorize]
        public IActionResult Id()
            => Ok(_authManager.GetCurrentUserId());

        /// <summary>
        /// Checks if given song is liked by user
        /// </summary>
        [HttpGet("{songId}")]
        [Authorize]
        public IActionResult IsSongLiked(
            int songId,
            [FromServices] IApplicationUserManager userManager)
            => Ok(userManager.IsSongLiked(_authManager.GetCurrentUserId(), songId));

        /// <summary>
        /// Checks if given musician is followed by current user
        /// </summary>
        [HttpGet("{musicianId}")]
        [Authorize]
        public IActionResult IsMusicianFollowed(
            int musicianId,
            [FromServices] IApplicationUserManager userManager)
            => Ok(userManager.IsMusicianFollowed(_authManager.GetCurrentUserId(), musicianId));

        /// <summary>
        /// Checks if given album is liked by current user
        /// </summary>
        [HttpGet("{albumId}")]
        [Authorize]
        public IActionResult IsAlbumLiked(
            int albumId,
            [FromServices] IApplicationUserManager userManager)
            => Ok(userManager.IsAlbumLiked(_authManager.GetCurrentUserId(), albumId));
    }
}
