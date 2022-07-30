
namespace Spotify.UI.Infrastructure.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveSongFile(IFormFile file);
        FileStream GetSongFile(string fileName);
        bool RemoveSongFile(string fileName);
    }
}
