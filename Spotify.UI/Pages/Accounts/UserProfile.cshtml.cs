using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.User;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages.Accounts
{
    public class UserProfileModel : PageModel
    {
        public UserInfo Info { get; set; }
        public IEnumerable<AlbumViewModel> LikedAlbums { get; set; }
        public IEnumerable<MusicianViewModel> FollowedMusicians { get; set; }

        public IActionResult OnGet(string id, [FromServices] GetUserProfile getUserProfile)
        {
            var user = getUserProfile.Execute(id);

            if (user is null)
                return RedirectToPage("/Index");

            Info = new UserInfo
            {
                Id = user.Id,
                Name = user.Name,
                LikedSongsCount = user.LikedSongsCount
            };

            FollowedMusicians = user.FollowedMusicians.Select(musician => new MusicianViewModel
            {
                Id = musician.Id,
                Name = musician.Name
            }).ToList();

            LikedAlbums = user.LikedAlbums.Select(album => new AlbumViewModel
            {
                Name = album.Name,
                CreatorName = album.CreatorName,
                Id = album.Id,
            }).ToList();

            return Page();
        }

        public class UserInfo
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public int LikedSongsCount { get; set; }
        }
    }
}
