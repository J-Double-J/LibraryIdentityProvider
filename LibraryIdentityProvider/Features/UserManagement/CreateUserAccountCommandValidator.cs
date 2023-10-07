using FluentValidation;
using LibraryIdentityProvider.Entities;
using LibraryIdentityProvider.Patterns.Validation;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Features.UserManagement
{
    public class CreateUserAccountCommandValidator : IdentityProviderValidator<CreateUserAccountCommand>
    {
        public CreateUserAccountCommandValidator()
            : base(LibraryIdentityValidatorType.Request, false)
        {
            RuleFor(command => command.Username).NotEmpty().MinimumLength(UserAccount.USERNAME_MIN_LENGTH);

            RuleFor(command => command.Password).NotEmpty().MinimumLength(UserAccount.PASSWORD_MIN_LENGTH);

            RuleFor(command => command.Email).NotEmpty().EmailAddress();

            RuleFor(command => command.FirstName).NotEmpty().MaximumLength(UserAccount.FIRSTNAME_MAX_LENGTH);

            RuleFor(command => command.LastName).NotEmpty().MaximumLength(UserAccount.LASTNAME_MAX_LENGTH);
        }
    }
}
