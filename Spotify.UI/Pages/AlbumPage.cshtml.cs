using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Albums;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages
{
    public class AlbumModel : PageModel
    {
        public IEnumerable<SongViewModel> Songs { get; private set; }
        public AlbumInfo Info { get; private set; }

        public IActionResult OnGet(int id, [FromServices] GetAlbum getAlbum)
        {
            var album = getAlbum.Execute(id);

            if (album is null)
                return RedirectToPage("Index");

            int index = 1;

            Songs = album.Songs.Select(song => new SongViewModel
            {
                Id = song.Id,
                AlbumId = song.AlbumId,
                CreatorId = song.CreatorId,
                CreatorName = song.CreatorName,
                Index = index++,
                Name = song.Name,
                Plays = song.Plays,
                FileName = song.FileName,
                AlbumFileName = song.AlbumFileName
            });

            Info = new AlbumInfo
            {
                Id = album.Id,
                Name = album.Name,
                SongCount = album.SongCount
            };

            return Page();
        }

        public class AlbumInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
        }
    }
}
