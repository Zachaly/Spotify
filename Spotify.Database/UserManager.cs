using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private AppDbContext _dbContext;

        public ApplicationUserManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ApplicationUser GetUserByEmail(string email) 
            => _dbContext.Users.FirstOrDefault(user => user.Email == email);

        public bool IsEmailOccupied(string email)
            => _dbContext.Users.AsEnumerable().Any(user => user.Email == email);
    }
}
