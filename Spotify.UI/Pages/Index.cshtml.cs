using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Albums;
using Spotify.Application.Musicians;

namespace Spotify.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetMusicians.MusicianModel> Musicians { get; set; }
        public IEnumerable<GetAlbums.AlbumModel> Albums { get; set; }

        public void OnGet([FromServices] GetMusicians getMusicians, [FromServices] GetAlbums getAlbums)
        {
            Musicians = getMusicians.Execute();
            Albums = getAlbums.Execute();
        }
    }
}