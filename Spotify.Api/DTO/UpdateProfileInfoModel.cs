using Microsoft.AspNetCore.Http;

namespace Spotify.Api.DTO
{
    public class UpdateProfileInfoModel
    {
        public string Username { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
