using FluentValidation.Results;
using Fluent = FluentValidation.Results;

namespace LibraryIdentityProvider.Patterns.ResultAndError
{
    public class IdentityFluentValidationResult : ValidationResult
    {
        public IdentityFluentValidationResult() { Errors = new(); }

        public IdentityFluentValidationResult(ValidationResult result, LibraryIdentityValidatorType libraryValidatorType)
        {
            Errors = result.Errors;
            LibraryValidatorType = libraryValidatorType;
        }

        public LibraryIdentityValidatorType LibraryValidatorType { get; set; }
    }
}
