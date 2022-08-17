using Spotify.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Spotify.Api.Infrastructure.AuthManager
{
    public interface IAuthManager
    {
        Task<JwtSecurityToken> GetToken(ApplicationUser user);
        bool IsMusicianManagerCorrect(string userId, int musicianId);
        bool IsSongManagerCorrect(string userId, int songId);
        bool IsAlbumManagerCorrect(string userId, int albumId);
        bool IsPlaylistCreator(string userId, int playlistId);
        string GetCurrentUserId();
        string GetHighestUserClaim(string userId);
    }
}
