using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spotify.UI.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public async Task<IActionResult> OnPost([FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] IApplicationUserManager appUserManager)
        {
            if (!ModelState.IsValid || appUserManager.IsEmailOccupied(Input.Email))
            {
                return Page();
            }

            var user = new ApplicationUser
            {
                Email = Input.Email,
                UserName = Input.Username
            };

            await userManager.CreateAsync(user, Input.Password);

            return RedirectToPage("Login");
        }

        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [MinLength(6)]
            public string Password { get; set; }
            [Required]
            [MinLength(10)]
            public string Username { get; set; }
        }
    }
}
