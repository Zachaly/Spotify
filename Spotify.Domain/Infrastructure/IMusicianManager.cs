﻿using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IMusicianManager
    {
        public IEnumerable<T> GetMusicians<T>(Func<Musician, T> selector);
        public T GetMusicianById<T>(int id, Func<Musician, T> selector);
        public Task<bool> AddMusicianAsync(string name, string description);
        public Task<bool> UpdateMusicianAsync(int id, string name, string description);
        public Task<bool > DeleteMusicianAsync(int id);
    }
}
