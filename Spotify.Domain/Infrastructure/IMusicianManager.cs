using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IMusicianManager
    {
        public IEnumerable<T> GetMusicians<T>(Func<Musician, T> selector);
        public T GetMusicianById<T>(int id, Func<Musician, T> selector);
        public Task<bool> AddMusicianAsync(Musician musician);
        public Task<bool> UpdateMusicianAsync(int id, string name, string description);
        public Task<bool > DeleteMusicianAsync(int id);
        public IEnumerable<T> GetMusiciansOfManager<T>(string managerId, Func<Musician, T> selector);
        IEnumerable<T> GetMusiciansByName<T>(string name, int count, Func<Musician, T> selector);
    }
}
