using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.Admin.Musicians;

namespace Spotify.UI.Pages.Admin
{
    public class MusicianModel : PageModel
    {
        public GetMusician.Response Musician { get; set; }

        public IActionResult OnGet(int id, [FromServices] GetMusician getMusician)
        {
            Musician = getMusician.Execute(id);

            if (Musician is null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
