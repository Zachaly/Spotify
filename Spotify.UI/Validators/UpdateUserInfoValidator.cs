using FluentValidation;
using Spotify.UI.Pages.Accounts;

namespace Spotify.UI.Validators
{
    public class UpdateUserInfoValidator : AbstractValidator<ChangeProfileInfoModel.UpdateModel>
    {
        public UpdateUserInfoValidator()
        {
            RuleFor(model => model.Username).NotEmpty().
                Length(10, 30).
                WithMessage("{PropertyName} must have between {MinLength} and {MaxLength} characters!").
                Matches("^[0-9a-zA-Z ]+$"). // works for english, but does not let usage of contry specific characters like ł, cyrilic etc.
                WithMessage("{PropertyName} must consist of only alphanumeric characters!");
        }
    }
}
