﻿using Spotify.Domain.Models;

namespace Spotify.Domain.Infrastructure
{
    public interface IAlbumsManager
    {
        Task<bool> AddAlbumAsync(Album album);
        Task<bool> RemoveAlbumAsync(int id);
        IEnumerable<T> GetAlbums<T>(Func<Album, T> selector);
        T GetAlbumById<T>(int id, Func<Album, T> selector);
        Task<bool> UpdateAlbumAsync(int id, Action<Album> changedValues);
        IEnumerable<T> GetTopAlbums<T>(int creatorId, int count, Func<Album, T> selector);
        IEnumerable<T> GetAlbumsOfMusician<T>(int musicianId, Func<Album, T> selector);
        IEnumerable<T> GetAlbumsOfManager<T>(string managerId, Func<Album, T> selector);
        IEnumerable<T> GetAlbumsByName<T>(string name, int count, Func<Album, T> selector);
    }
}
