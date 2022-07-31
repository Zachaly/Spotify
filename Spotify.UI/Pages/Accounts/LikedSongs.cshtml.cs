using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.User;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages.Accounts
{
    public class LikedSongsModel : PageModel
    {
        public IEnumerable<SongViewModel> Songs { get; set; }
        public IActionResult OnGet(string id, [FromServices] GetUserLikedSongs getSongs)
        {
            int index = 0;
            Songs = getSongs.Execute(id).Select(song => new SongViewModel
            {
                Id = song.Id,
                AlbumId = song.AlbumId,
                CreatorId = song.CreatorId,
                CreatorName = song.CreatorName,
                Index = ++index,
                Name = song.Name,
                Plays = 0,
                FileName = song.FileName,
            }).ToList();

            if (Songs is null)
                return RedirectToPage("/Index");

            return Page();
        }
    }
}