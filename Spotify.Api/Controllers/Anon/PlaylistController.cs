using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Playlists;

namespace Spotify.Api.Controllers.Anon
{
    [Route("/api/Anon/[controller]")]
    public class PlaylistController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Playlist(int id, [FromServices] GetPlaylist getPlaylist)
            => Ok(getPlaylist.Execute(id));

        [HttpGet("/User/{userId}")]
        public IActionResult UserPlaylists(string userId, [FromServices] GetUserPlaylists getPlaylists)
            => Ok(getPlaylists.Execute(userId));
    }
}
