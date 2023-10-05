using FluentValidation;
using FluentValidation.Results;
using LibraryIdentityProvider.Patterns.ResultAndError;

namespace LibraryIdentityProvider.Patterns.Validation
{
    public class IdentityProviderValidator<T> : AbstractValidator<T>
    {
        public IdentityProviderValidator(LibraryIdentityValidatorType identityValidatorType, bool isSlowValidator = false)
        {
            LibraryIdentityValidatorType = identityValidatorType;
            IsSlowValidator = isSlowValidator;
        }

        public LibraryIdentityValidatorType LibraryIdentityValidatorType { get; init; }

        /// <summary>
        /// Gets or sets if a validator would be considered 'slow'.
        /// </summary>
        /// <remarks>
        /// A slow validator is one that might wait on IO/Database/HTTP to validate an object.
        /// </remarks>
        public bool IsSlowValidator { get; private set; }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default)
        {
            ValidationResult validationResult = await base.ValidateAsync(context, cancellation);

            return validationResult.ToIdentityFluentValidationResult(LibraryIdentityValidatorType);
        }

        public async Task<DomainValidationResult<T>> ValidateAsyncGetDomainResult(T instance, CancellationToken cancellation = default)
        {
            ValidationResult validationResult = await ValidateAsync(instance, cancellation);

            return validationResult.ToDomainValidationResult(instance, LibraryIdentityValidatorType);
        }
    }
}
