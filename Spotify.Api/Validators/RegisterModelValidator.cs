using FluentValidation;
using Spotify.Api.DTO;
using Spotify.Domain.Infrastructure;

namespace Spotify.Api.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly IApplicationUserManager _appUserManager;

        public RegisterModelValidator(IApplicationUserManager appUserManager)
        {
            _appUserManager = appUserManager;
            RuleFor(model => model.Email).NotEmpty().
                EmailAddress().
                WithMessage("{PropertyValue} is not a valid e-mail address!").
                Must(IsEmailAvailable).
                WithMessage("Thsi e-mail is already occupied!");

            RuleFor(model => model.Username).NotEmpty().
                Length(10, 30).
                WithMessage("{PropertyName} must have between {MinLength} and {MaxLength} characters!").
                Matches("^[0-9a-zA-Z ]+$"). // works for english, but does not let usage of contry specific characters like ł, cyrilic etc.
                WithMessage("{PropertyName} must consist of only alphanumeric characters!");

            RuleFor(model => model.Password).NotEmpty().
                MinimumLength(6).
                WithMessage("{PropertyName} must have at least 6 characters").
                Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&].{6,}$").
                WithMessage("{PropertyName} must have at least one lowercase and uppercase letter, number and a special character");

        }

        private bool IsEmailAvailable(string email) => !_appUserManager.IsEmailOccupied(email);
    }
}
