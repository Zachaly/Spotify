using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Songs;
using Spotify.Application.User;
using Spotify.Domain.Models;
using Spotify.Api.Infrastructure.FileManager;
using System.Security.Claims;
using Spotify.Api.DTO;
using Spotify.Api.Validators;
using Spotify.Domain.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Spotify.Api.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly string _authAudience;
        private readonly string _authIssuer;
        private readonly string _secret;

        public UserController(IConfiguration configuration)
        {
            _authAudience = configuration["AuthAudience"];
            _authIssuer = configuration["AuthIssuer"];
            _secret = configuration["Secret"];
        }

        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<ApplicationUser> signInManager)
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPlay(int id, [FromServices] AddPlay addPlay)
            => Ok(await addPlay.Execute(id));

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SetDefaultProfilePicture(
            string userId,
            [FromServices] SetDefaultProfilePicture setDefaultPicture,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            fileManager.RemoveProfilePicture(getFileName.Execute(userId));

            await setDefaultPicture.Execute(userId);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddManager([FromServices] UserManager<ApplicationUser> userManager)
        {
            if (!(User.Identity as ClaimsIdentity).HasClaim("Role", "Manager"))
                await userManager.AddClaimAsync(await userManager.GetUserAsync(User), new Claim("Role", "Manager"));

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel,
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

        [HttpPost("{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password,
            [FromServices] IApplicationUserManager appUserManager,
            [FromServices] UserManager<ApplicationUser> userManager)
        {
            var user = appUserManager.GetUserByEmail(email);

            if (user is null)
                return BadRequest("Username or password is incorrect");

            var result = await userManager.CheckPasswordAsync(user, password);

            if (!result)
                return BadRequest("Username or password is incorrect");


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            };

            claims.AddRange(await userManager.GetClaimsAsync(user));

            var bytes = Encoding.UTF8.GetBytes(_secret);
            var key = new SymmetricSecurityKey(bytes);

            var algorithm = SecurityAlgorithms.HmacSha256;

            var credentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                _authAudience,
                _authIssuer,
                claims,
                DateTime.Now,
                DateTime.Now.AddHours(8),
                credentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { auth_token = tokenJson});
        }

        [HttpGet("{id}")]
        public IActionResult Profile(string id, [FromServices] GetUserProfile getUser)
            => Ok(getUser.Execute(id));

        [HttpGet("{id}")]
        public IActionResult UserLikedSongs(string id, [FromServices] GetUserLikedSongs getSongs)
            => Ok(getSongs.Execute(id));

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateProfileInfoModel request,
            [FromServices] UpdateUser updateUser,
            [FromServices] IFileManager fileManager,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] UpdateProfileInfoValidator validator)
        {
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage);
                return BadRequest(errors);
            }

            var currentPicture = getFileName.Execute(request.Id);

            if (request.ProfilePicture != null && currentPicture != "placeholder.jpg")
                fileManager.RemoveProfilePicture(currentPicture);

            await updateUser.Execute(new UpdateUser.Request
            {
                FileName = await fileManager.SaveProfilePicture(request.ProfilePicture),
                UserName = request.Username,
                Id = request.Id
            });

            return Ok();
        }
    }
}
