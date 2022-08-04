using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Playlists;
using Spotify.Application.User;
using Spotify.Domain.Models;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages.Accounts
{
    public class UserProfileModel : PageModel
    {
        public UserInfo Info { get; set; }
        public IEnumerable<AlbumViewModel> LikedAlbums { get; set; }
        public IEnumerable<PlaylistViewModel> Playlists { get; private set; }
        public IEnumerable<MusicianViewModel> FollowedMusicians { get; set; }

        public IActionResult OnGet(string id,
            [FromServices] GetUserProfile getUserProfile,
            [FromServices] GetUserPlaylists getUserPlaylists,
            [FromServices] UserManager<ApplicationUser> userManager)
        {
            if (id is null)
                id = userManager.GetUserId(HttpContext.User);

            var user = getUserProfile.Execute(id);

            if (user is null)
                return RedirectToPage("/Index");

            Info = new UserInfo
            {
                Id = user.Id,
                Name = user.Name,
                LikedSongsCount = user.LikedSongsCount,
                FileName = user.FileName,
            };

            FollowedMusicians = user.FollowedMusicians.Select(musician => new MusicianViewModel
            {
                Id = musician.Id,
                Name = musician.Name,
                FileName = musician.FileName,
            }).ToList();

            LikedAlbums = user.LikedAlbums.Select(album => new AlbumViewModel
            {
                Name = album.Name,
                CreatorName = album.CreatorName,
                Id = album.Id,
                FileName = album.FileName,
            }).ToList();

            Playlists = getUserPlaylists.Execute(id).Select(playlist => new PlaylistViewModel
            {
                CreatorName = playlist.CreatorName,
                FileName = playlist.FileName,
                Id = playlist.Id,
                Name = playlist.Name,
                SongCount = playlist.SongCount
            });

            return Page();
        }

        public class UserInfo
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public int LikedSongsCount { get; set; }
            public string FileName { get; set; }
        }
    }
}
