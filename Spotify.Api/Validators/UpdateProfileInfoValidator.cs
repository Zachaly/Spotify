using FluentValidation;
using Spotify.Api.DTO;
using Spotify.Api.Infrastructure;

namespace Spotify.Api.Validators
{
    public class UpdateProfileInfoValidator : AbstractValidator<UpdateProfileInfoModel>
    {
        public UpdateProfileInfoValidator()
        {
            RuleFor(model => model.Username).NotEmpty().
                Length(10, 30).
                WithMessage("{PropertyName} must have between {MinLength} and {MaxLength} characters!").
                Matches("^[0-9a-zA-Z ]+$"). // works for english, but does not let usage of contry specific characters like ł, cyrilic etc.
                WithMessage("{PropertyName} must consist of only alphanumeric characters!");

            RuleFor(model => model.ProfilePicture).
                Must(file => file.IsExtensionCorrect(".jpg")).
                WithMessage("Invalid file extension! Expected: .jpg");
        }
    }
}
