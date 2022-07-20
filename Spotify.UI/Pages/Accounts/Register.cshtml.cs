using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Domain.Models;

namespace Spotify.UI.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public async Task<IActionResult> OnPost(
            [FromServices] UserManager<ApplicationUser> userManager,
            [FromServices] IValidator<RegisterViewModel> validator)
        {
            var result = validator.Validate(Input);

            if (!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
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
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string Username { get; set; }
        }
    }
}
