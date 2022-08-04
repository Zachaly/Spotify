using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Playlists;
using Spotify.UI.Infrastructure.FileManager;

namespace Spotify.UI.Pages
{
    public class UpdatePlaylistModel : PageModel
    {
        [BindProperty]
        public UpdatePlaylistInput Input { get; set; }
        public IActionResult OnGet(int id,[FromServices] GetPlaylist getPlaylist)
        {
            var playlist = getPlaylist.Execute(id);
            Input = new UpdatePlaylistInput
            {
                Id = id,
                Name = playlist.Name,
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(
            [FromServices] UpdatePlaylist updatePlaylist,
            [FromServices] GetPlaylistFileName getFileName,
            [FromServices] IFileManager fileManager)
        {
            string fileName = await fileManager.SavePlaylistPicture(Input.File);

            if (!string.IsNullOrEmpty(fileName))
                fileManager.RemovePlaylistPicture(getFileName.Execute(Input.Id));

            await updatePlaylist.Execute(new UpdatePlaylist.Request
            {
                FileName = fileName,
                Id = Input.Id,
                Name = Input.Name
            });

            return RedirectToPage("PlaylistPage", new { id = Input.Id });
        }

        public class UpdatePlaylistInput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IFormFile File { get; set; }
        }
    }
}
