using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
