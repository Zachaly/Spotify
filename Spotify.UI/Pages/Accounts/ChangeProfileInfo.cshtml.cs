using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spotify.Application.User;
using Spotify.Domain.Models;
using Spotify.UI.Infrastructure.FileManager;
using Spotify.UI.Validators;

namespace Spotify.UI.Pages.Accounts
{
    public class ChangeProfileInfoModel : PageModel
    {
        [BindProperty]
        public UpdateModel Input { get; set; }

        public IActionResult OnGet(string id, [FromServices] UserManager<ApplicationUser> userManager)
        {
            Input = new UpdateModel
            {
                Username = userManager.GetUserName(HttpContext.User),
                Id = id,
            };
            return Page();
        }

        public async Task<IActionResult> OnPost(
            [FromServices] UpdateUser updateUser,
            [FromServices] IFileManager fileManager,
            [FromServices] GetProfilePictureFileName getFileName,
            [FromServices] UpdateUserInfoValidator validator)
        {
            var result = validator.Validate(Input);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return Page();
            }

            var currentPicture = getFileName.Execute(Input.Id);

            if (Input.ProfilePicture != null && currentPicture != "placeholder.jpg")
                fileManager.RemoveProfilePicture(currentPicture);

            await updateUser.Execute(new UpdateUser.Request
            {
                FileName = await fileManager.SaveProfilePicture(Input.ProfilePicture),
                UserName = Input.Username,
                Id = Input.Id
            });

            return RedirectToPage("UserProfile", new { id = Input.Id });
        }

        public class UpdateModel
        {
            public string Username { get; set; }
            public IFormFile ProfilePicture { get; set; }
            public string Id { get; set; }
        }
    }
}
