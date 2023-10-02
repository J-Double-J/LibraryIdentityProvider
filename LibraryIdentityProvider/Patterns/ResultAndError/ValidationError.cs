namespace LibraryIdentityProvider.Patterns.ResultAndError
{
    /// <summary>
    /// Class for Validation Errors.
    /// </summary>
    public class ValidationError : Error
    {

        /// <summary>
        /// Validation Error for constructor.
        /// </summary>
        /// <param name="messageHeader">String to display in message before error. Note that a new line is inserted at the end.</param>
        /// <param name="errorMessage">Error message for validation.</param>
        public ValidationError(ErrorCode errorCode, string errorMessage)
            : base(errorCode, errorMessage)
        {
            DetermineIfValidationLevelIsInternal(errorCode);
        }

        public ValidationError(ErrorCode errorCode, string errorMessage, bool isInternal)
            : base(errorCode, errorMessage, isInternal)
        {
        }

        /// <summary>
        /// Determines if the error code type is an internal error. This can be used to mask errors when returned in the API.
        /// </summary>
        /// <param name="validationLevel">What level the error occurred at.</param>
        /// <exception cref="InvalidOperationException">Throws if a <see cref="LibraryValidatorType.Mixed"/> is passed in. Errors
        /// must </exception>
        private void DetermineIfValidationLevelIsInternal(ErrorCode errorCode)
        {
            IsInternalError = false;
        }
    }
}
