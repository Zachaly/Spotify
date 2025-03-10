﻿using FluentValidation;
using Spotify.Domain.Infrastructure;
using Spotify.UI.Pages.Accounts;

namespace Spotify.UI.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterModel.RegisterViewModel>
    {
        private readonly IApplicationUserManager _appUserManager;

        public RegisterViewModelValidator(IApplicationUserManager appUserManager)
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

            RuleFor(model => model.ConfirmPassword).NotEmpty().
                Equal(model => model.ConfirmPassword).
                WithMessage("Passwords are not the same!");
        }

        private bool IsEmailAvailable(string email) => !_appUserManager.IsEmailOccupied(email);
    }
}
