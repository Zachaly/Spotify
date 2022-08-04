using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Playlists;
using Spotify.UI.ViewModels;

namespace Spotify.UI.Pages
{
    public class PlaylistPageModel : PageModel
    {
        public IEnumerable<SongViewModel> Songs { get; private set; }
        public PlaylistInfo Info { get; private set; }

        public IActionResult OnGet(int id, [FromServices] GetPlaylist getPlaylist)
        {
            var playlist = getPlaylist.Execute(id);

            if (playlist is null)
                return RedirectToPage("Index");

            int index = 1;

            Songs = playlist.Songs.Select(song => new SongViewModel
            {
                Id = song.Id,
                AlbumFileName = song.AlbumFileName,
                AlbumId = song.AlbumId,
                CreatorId = song.CreatorId,
                CreatorName = song.CreatorName,
                FileName = song.FileName,
                Index = index++,
                Name = song.Name,
                Plays = 0
            });

            Info = new PlaylistInfo
            {
                CreatorId = playlist.CreatorId,
                CreatorName = playlist.CreatorName,
                FileName = playlist.FileName,
                Name = playlist.Name,
                SongCount = playlist.Songs.Count(),
                Id = playlist.Id
            };

            return Page();
        }

        public class PlaylistInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
            public string CreatorId { get; set; }
            public string FileName { get; set; }
            public int SongCount { get; set; }
        }
    }
}
