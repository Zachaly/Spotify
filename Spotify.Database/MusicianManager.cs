using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class MusicianManager : IMusicianManager
    {
        private AppDbContext _dbContext;

        public MusicianManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddMusicianAsync(Musician musician)
        {
            _dbContext.Musicians.Add(musician);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMusicianAsync(int id)
        {
            var musician = _dbContext.Musicians.FirstOrDefault(x => x.Id == id);
            _dbContext.Musicians.Remove(musician);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public T GetMusicianById<T>(int id, Func<Musician, T> selector)
            => _dbContext.Musicians.Include(db => db.Followers).Include(db => db.Songs).
            Include(db => db.Albums).ThenInclude(db => db.Songs).
            Where(musician => musician.Id == id).
            Select(selector).
            FirstOrDefault();

        public IEnumerable<T> GetMusicians<T>(Func<Musician, T> selector) 
            => _dbContext.Musicians.Include(db => db.Followers).Include(db => db.Songs).
            Include(db => db.Albums).ThenInclude(db => db.Songs).
            Select(selector).AsEnumerable();

        public async Task<bool> UpdateMusicianAsync(int id,string name, string description)
        {
            var musician = _dbContext.Musicians.FirstOrDefault(musician => musician.Id == id);

            musician.Name = name;
            musician.Description = description;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
