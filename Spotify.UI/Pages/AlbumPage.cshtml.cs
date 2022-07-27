using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Albums;

namespace Spotify.UI.Pages
{
    public class AlbumModel : PageModel
    {
        public GetAlbum.Response Album { get; private set; }

        public IActionResult OnGet(int id, [FromServices] GetAlbum getAlbum)
        {
            Album = getAlbum.Execute(id);

            if (Album is null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
