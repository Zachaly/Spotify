
namespace Spotify.UI.Infrastructure.FileManager
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
    }
}
