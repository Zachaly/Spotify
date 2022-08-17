using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Api.Infrastructure.AuthManager
{
    public class AuthManager : IAuthManager
    {
        private readonly string _authAudience;
        private readonly string _authIssuer;
        private readonly string _secretKey;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISongsManager _songsManager;
        private readonly IApplicationUserManager _appUserManager;
        private readonly IAlbumsManager _albumsManager;
        private readonly IPlaylistManager _playlistManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManager(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            ISongsManager songsManager,
            IApplicationUserManager appUserManager,
            IAlbumsManager albumsManager,
            IPlaylistManager playlistManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _authAudience = config["AuthAudience"];
            _authIssuer = config["AuthIssuer"];
            _secretKey = config["Secret"];
            _userManager = userManager;
            _songsManager = songsManager;
            _appUserManager = appUserManager;
            _albumsManager = albumsManager;
            _playlistManager = playlistManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JwtSecurityToken> GetToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            };

            claims.AddRange(await _userManager.GetClaimsAsync(user));

            var bytes = Encoding.UTF8.GetBytes(_secretKey);
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

            return token;
        }

        public bool IsMusicianManagerCorrect(string userId, int musicianId)
            => _appUserManager.IsUserManagerOfMusician(userId, musicianId)
            || _httpContextAccessor.HttpContext.User.HasClaim("Role", "Admin");

        public bool IsSongManagerCorrect(string userId, int songId)
            => IsMusicianManagerCorrect(userId, _songsManager.GetSongById(songId, song => song.MusicianId));

        public bool IsAlbumManagerCorrect(string userId, int albumId)
            => IsMusicianManagerCorrect(userId, _albumsManager.GetAlbumById(albumId, album => album.MusicianId));

        public bool IsPlaylistCreator(string userId, int playlistId)
            => userId == _playlistManager.GetPlaylist(playlistId, playlist => playlist.CreatorId);

        public string GetCurrentUserId()
            => _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);

        public string GetHighestUserClaim(string userId)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.HasClaim("Role", "Admin"))
                return "Admin";

            if (user.HasClaim("Role", "Manager"))
                return "Manager";

            return "";
        }
    }
}
