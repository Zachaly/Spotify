using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Albums;
using Spotify.Application.Musicians;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<MusicianViewModel> Musicians { get; set; }
        public IEnumerable<AlbumViewModel> Albums { get; set; }

        public void OnGet([FromServices] GetMusicians getMusicians, [FromServices] GetAlbums getAlbums)
        {
            Musicians = getMusicians.Execute().Select(musician => new MusicianViewModel
            {
                Id = musician.Id,
                Name = musician.Name,
                Plays = musician.Plays,
                FileName = musician.FileName,
            }).ToList();
            Albums = getAlbums.Execute().Select(album => new AlbumViewModel
            {
                Id = album.Id,
                CreatorName = album.CreatorName,
                Name = album.Name,
                Plays = album.Plays,
                FileName = album.FileName
            });
        }
    }
}