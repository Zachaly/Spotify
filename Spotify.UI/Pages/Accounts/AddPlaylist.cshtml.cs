using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Playlists;
using Spotify.Domain.Models;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Pages.Accounts
{
    public class AddPlaylistModel : PageModel
    {
        [BindProperty]
        public PlaylistInput Input { get; set; }

        public IActionResult OnGet()
        {
            Input = new PlaylistInput
            {
                Name = "New Playlist"
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] AddPlaylist addPlaylist,
            [FromServices] IFileManager fileManager)
        {
            var userId = userManager.GetUserId(HttpContext.User);

            string fileName = "placeholder.jpg";

            if (Input.File != null)
                fileName = await fileManager.SavePlaylistPicture(Input.File);

            await addPlaylist.Execute(new AddPlaylist.Request
            {
                Name = Input.Name,
                UserId = userId,
                FileName = fileName
            });

            return RedirectToPage("/Accounts/UserProfile", new { id = userId });
        }

        public class PlaylistInput
        {
            public string Name { get; set; }
            public IFormFile File { get; set; }
        }
    }
}
