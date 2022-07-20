using Spotify.Domain.Models;
namespace Spotify.Domain.Infrastructure
{
    public interface IApplicationUserManager
    {
        public ApplicationUser GetUserByEmail(string email);
        public bool IsEmailOccupied(string email);
    }
}
