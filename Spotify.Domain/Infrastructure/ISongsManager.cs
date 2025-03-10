﻿using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface ISongsManager
    {
        IEnumerable<T> GetSongs<T>(Func<Song, T> selector);
        T GetSongById<T>(int id, Func<Song, T> selector);
        Task<bool> AddSongAsync(Song song);
        Task<bool> RemoveSongAsync(int id);
        Task<bool> UpdateSongAsync(int id, Action<Song> updateValues);
        IEnumerable<T> GetTopSongs<T>(int creatorId, int count, Func<Song, T> selector);
        Task<bool> AddPlay(int id);
        IEnumerable<T> GetSongsOfManager<T>(string id, Func<Song, T> selector);
        IEnumerable<T> GetSongsByName<T>(string name, int count, Func<Song, T> selector);
    }
}
