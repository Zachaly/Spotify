using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.UI.Infrastructure.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _songPath;
        public FileManager(IConfiguration configuration)
        {
            _songPath = configuration["SongFiles"];
        }

        public FileStream GetSongFile(string fileName)
            => new FileStream(Path.Combine(_songPath, fileName), FileMode.Open, FileAccess.Read);

        public bool RemoveSongFile(string fileName)
        {
            try
            {
                var file = Path.Combine(_songPath, fileName);
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

        private string GenerateRandomSongName()
        {
            var random = new Random();
            string signs = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            char[] result = new char[10];

            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = signs[random.Next(signs.Length)];
                }
            } while (File.Exists(Path.Combine(_songPath, new string(result))));

            return new string(result);
        }

        public async Task<string> SaveSongFile(IFormFile file)
        {
            try
            {
                var mime = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                var fileName = GenerateRandomSongName() + mime;

                using (var stream = File.Create(Path.Combine(_songPath, fileName)))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
