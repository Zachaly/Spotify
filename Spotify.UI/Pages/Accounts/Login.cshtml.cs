using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spotify.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPost([FromServices] SignInManager<ApplicationUser> signInManager,
            [FromServices] IApplicationUserManager appUserManager)
        {
            if (!ModelState.IsValid)
                return Page();

            var user = appUserManager.GetUserByEmail(Input.Email);

            await signInManager.PasswordSignInAsync(user, Input.Password, false, false);

            return RedirectToPage("../Index");
        }

        public class LoginViewModel
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
