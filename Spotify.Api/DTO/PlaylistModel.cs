using Microsoft.AspNetCore.Http;

namespace Spotify.Api.DTO
{
    public class PlaylistModel
    {
        public string Name { get; set; }
        public IFormFile? CoverPicture { get; set; }
    }
}
