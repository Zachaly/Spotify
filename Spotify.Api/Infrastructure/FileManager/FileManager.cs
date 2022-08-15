
namespace Spotify.Api.Infrastructure.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _songPath;
        private readonly string _albumPath;
        private readonly string _musicianPath;
        private readonly string _profilePicPath;
        private readonly string _playlistPath;

        public FileManager(IConfiguration configuration)
        {
            _songPath = configuration["SongFiles"];
            _albumPath = configuration["AlbumCovers"];
            _musicianPath = configuration["BandPictures"];
            _profilePicPath = configuration["UserProfilePictures"];
            _playlistPath = configuration["PlaylistPictures"];
        }

        // helper functions
        private async Task<string> SaveFile(IFormFile file, string path)
        {
            try
            {
                var mime = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                var fileName = $"{Guid.NewGuid()}{mime}";

                using (var stream = File.Create(Path.Combine(path, fileName)))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private FileStream GetFile(string fileName, string path)
            => new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read);

        private bool RemoveFile(string fileName, string path)
        {
            if (fileName == "placeholder.jpg")
                return true;

            try
            {
                var file = Path.Combine(path, fileName);
                if (File.Exists(file))
                {
                    File.Delete(file);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // song functions
        public FileStream GetSongFile(string fileName) => GetFile(fileName, _songPath);

        public bool RemoveSongFile(string fileName) => RemoveFile(fileName, _songPath);

        public async Task<string> SaveSongFile(IFormFile file) => await SaveFile(file, _songPath);

        // album functions
        public async Task<string> SaveAlbumFile(IFormFile file) => await SaveFile(file, _albumPath);

        public FileStream GetAlbumFile(string fileName) => GetFile(fileName, _albumPath);

        public bool RemoveAlbumFile(string fileName) => RemoveFile(fileName, _albumPath);

        // musician functions
        public async Task<string> SaveMusicianFile(IFormFile file) => await SaveFile(file, _musicianPath);

        public FileStream GetMusicianFile(string fileName) => GetFile(fileName, _musicianPath);

        public bool RemoveMusicianFile(string fileName) => RemoveFile(fileName, _musicianPath);

        // user profile functions
        public Task<string> SaveProfilePicture(IFormFile file) => SaveFile(file, _profilePicPath);

        public FileStream GetProfilePicture(string fileName) => GetFile(fileName, _profilePicPath);

        public bool RemoveProfilePicture(string fileName) => RemoveFile(fileName, _profilePicPath);

        // playlist functions
        public Task<string> SavePlaylistPicture(IFormFile file) => SaveFile(file, _playlistPath);

        public FileStream GetPlaylistPicture(string fileName) => GetFile(fileName, _playlistPath);

        public bool RemovePlaylistPicture(string fileName) => RemoveFile(fileName, _playlistPath);
    }
}
