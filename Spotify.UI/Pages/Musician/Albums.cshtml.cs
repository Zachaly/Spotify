using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Musicians;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages.Musician
{
    public class AlbumsModel : PageModel
    {
        public IEnumerable<AlbumViewModel> Albums { get; set; }

        public IActionResult OnGet(int id, [FromServices] GetMusicianAlbums getAlbums)
        {
            Albums = getAlbums.Execute(id).Select(album => new AlbumViewModel 
            { 
                Id = album.Id,
                Name = album.Name,
                CreatorName = album.Name,
                FileName = album.FileName,
            });

            return Page();
        }
    }
}
