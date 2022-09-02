using Microsoft.AspNetCore.Http;

namespace Spotify.Api.Infrastructure
{
    public static class FileExtentions
    {
        public static bool IsExtensionCorrect(this IFormFile @this, string expectedExtention)
        {
            var mime = @this.FileName.Substring(@this.FileName.LastIndexOf('.'));

            return mime == expectedExtention;
        }
    }
}
