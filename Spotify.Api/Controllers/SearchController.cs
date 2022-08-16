using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Albums;
using Spotify.Application.Musicians;
using Spotify.Application.Playlists;
using Spotify.Application.Songs;
using Spotify.Application.User;

namespace Spotify.UI.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        /// <summary>
        /// Looks for musicians with similar name as given string
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("{name}")]
        public IActionResult Musicians(string name, [FromServices] SearchMusicians searchMusicians)
            => Ok(searchMusicians.Execute(name));

        /// <summary>
        /// Looks for songs with similar name as given string
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("{name}")]
        public IActionResult Songs(string name, [FromServices] SearchSongs searchSongs)
            => Ok(searchSongs.Execute(name));

        /// <summary>
        /// Looks for albums with similar name as given string
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("{name}")]
        public IActionResult Albums(string name, [FromServices] SearchAlbums searchAlbums)
            => Ok(searchAlbums.Execute(name));

        /// <summary>
        /// Looks for users with similar name as given string
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("{name}")]
        public IActionResult Users(string name, [FromServices] SearchUsers searchUsers)
            => Ok(searchUsers.Execute(name));

        /// <summary>
        /// Looks for playlists with similar name as given string
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("{name}")]
        public IActionResult Playlists(string name, [FromServices] SearchPlaylists searchPlaylists)
            => Ok(searchPlaylists.Execute(name));
    }
}
