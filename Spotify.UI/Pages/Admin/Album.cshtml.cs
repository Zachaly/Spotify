using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Admin.Albums;

namespace Spotify.UI.Pages.Admin
{
    public class AlbumModel : PageModel
    {
        public GetAlbum.Response Album { get; set; }

        public IActionResult OnGet(int id, [FromServices] GetAlbum getAlbum)
        {
            Album = getAlbum.Execute(id);

            if(Album == null)
                return RedirectToPage("Albums");

            return Page();
        }
    }
}
