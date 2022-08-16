using Microsoft.AspNetCore.Http;

namespace Spotify.Api.DTO
{
    public class UpdateProfileInfoModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
