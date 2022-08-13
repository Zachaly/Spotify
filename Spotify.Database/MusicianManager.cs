using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class MusicianManager : IMusicianManager
    {
        private readonly AppDbContext _dbContext;

        public MusicianManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IEnumerable<Musician> GetMusicians()
            => _dbContext.Musicians.
                Include(musician => musician.Followers).
                Include(musician => musician.Songs).
                Include(musician => musician.Albums).ThenInclude(album => album.Songs).
                AsEnumerable();

        private IEnumerable<Musician> GetMusiciansWithCondition(Func<Musician, bool> condition)
            => GetMusicians().Where(musician => condition(musician));

        public async Task<bool> AddMusicianAsync(Musician musician)
        {
            _dbContext.Musicians.Add(musician);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMusicianAsync(int id)
        {
            var musician = _dbContext.Musicians.FirstOrDefault(x => x.Id == id);

            _dbContext.Songs.RemoveRange(_dbContext.Songs.Where(x => x.MusicianId == id).ToList());
            _dbContext.Musicians.Remove(musician);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public T GetMusicianById<T>(int id, Func<Musician, T> selector)
            => GetMusiciansWithCondition(musician => musician.Id == id).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetMusicians<T>(Func<Musician, T> selector) 
            => GetMusicians().Select(selector);

        public IEnumerable<T> GetMusiciansByName<T>(string name, int count, Func<Musician, T> selector)
        => GetMusiciansWithCondition(musician => musician.Name.IsSimiliar(name)).
            Select(musician => new { Musician = musician, Distance = musician.Name.LevenshteinDistance(name) }).
            OrderBy(musicianWithDistance => musicianWithDistance.Distance).
            Take(count).
            Select(musicianWithDistance => musicianWithDistance.Musician).
            Select(selector);

        public IEnumerable<T> GetMusiciansOfManager<T>(string managerId, Func<Musician, T> selector)
            => GetMusiciansWithCondition(musician => musician.ManagerId == managerId).
                Select(selector);

        public async Task<bool> UpdateMusicianAsync(int id,string name, string description)
        {
            var musician = _dbContext.Musicians.FirstOrDefault(musician => musician.Id == id);

            musician.Name = name;
            musician.Description = description;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
