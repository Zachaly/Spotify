using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Musicians;

namespace Spotify.UI.Pages.Musician
{
    public class AlbumsModel : PageModel
    {
        public IEnumerable<GetMusicianAlbums.AlbumModel> Albums { get; set; }

        public IActionResult OnGet(int id, [FromServices] GetMusicianAlbums getAlbums)
        {
            Albums = getAlbums.Execute(id);

            return Page();
        }
    }
}
