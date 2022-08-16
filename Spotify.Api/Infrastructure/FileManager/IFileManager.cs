using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Spotify.Api.Infrastructure.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveSongFile(IFormFile file);
        FileStream GetSongFile(string fileName);
        bool RemoveSongFile(string fileName);

        Task<string> SaveAlbumFile(IFormFile file);
        FileStream GetAlbumFile(string fileName);
        bool RemoveAlbumFile(string fileName);

        Task<string> SaveMusicianFile(IFormFile file);
        FileStream GetMusicianFile(string fileName);
        bool RemoveMusicianFile(string fileName);

        Task<string> SaveProfilePicture(IFormFile file);
        FileStream GetProfilePicture(string fileName);
        bool RemoveProfilePicture(string fileName);

        Task<string> SavePlaylistPicture(IFormFile file);
        FileStream GetPlaylistPicture(string fileName);
        bool RemovePlaylistPicture(string fileName);
    }
}
