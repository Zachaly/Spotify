using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Musicians;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages.Musician
{
    public class ProfileModel : PageModel
    {
        public MusicianInfo Info { get; set; }
        public IEnumerable<SongViewModel> TopSongs { get; set; }
        public IEnumerable<AlbumViewModel> TopAlbums { get; set; }

        public IActionResult OnGet(int id, [FromServices] GetMusician getMusician)
        {
            var musician = getMusician.Execute(id);

            if (musician is null)
                return RedirectToPage("Index");

            Info = new MusicianInfo
            {
                Id = musician.Id,
                Description = musician.Description,
                Name = musician.Name,
                NumberOfFollowers = musician.NumberOfFollowers
            };

            var index = 1;

            TopSongs = musician.TopSongs.Select(song => new SongViewModel
            {
                Id = song.Id,
                Index = index++,
                Name = song.Name,
                Plays = song.Plays,
                CreatorName = song.CreatorName,
                CreatorId = musician.Id,
                AlbumId = song.AlbumId,
                FileName = song.FileName
            });

            TopAlbums = musician.TopAlbums.Select(album => new AlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                CreatorName = musician.Name
            });

            return Page();
        }

        public class MusicianInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int NumberOfFollowers { get; set; }
        }
    }
}
