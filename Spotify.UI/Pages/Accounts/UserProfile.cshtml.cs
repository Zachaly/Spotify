using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.User;

namespace Spotify.UI.Pages.Accounts
{
    public class UserProfileModel : PageModel
    {
        public GetUserProfile.Response User { get; set; }

        public IActionResult OnGet(string id, [FromServices] GetUserProfile getUserProfile)
        {
            User = getUserProfile.Execute(id);

            if (User is null)
                return RedirectToPage("/Index");

            return Page();
        }
    }
}
