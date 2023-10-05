namespace LibraryIdentityProvider.Patterns.ResultAndError
{
    /// <summary>
    /// This enum clarifies at what level of the application the validator is concerned with.
    /// This also means that all validation rules internally should be as relevant to the level as possible.
    /// </summary>
    public enum LibraryIdentityValidatorType
    {
        None = 0,

        Entity = 10,

        Database = 20,

        ExternalCommunication = 30,

        IO = 40,

        Configuration = 50,

        Request = 60,

        // Mixed means that a validator could have various levels and the error code will be
        // set at the validation result level.
        Mixed = 98,

        Unknown = 99
    }

    public static class ValidationErrorCodeFactory
    {
        public const string ValidationCodeRoot = "ValidationFailure";
        public const string EntityErrorCode = $"{ValidationCodeRoot}.Entity";
        public const string DatabaseErrorCode = $"{ValidationCodeRoot}.Database";
        public const string ExternalCommunicationErrorCode = $"{ValidationCodeRoot}.ExternalCommunication";
        public const string IOErrorCode = $"{ValidationCodeRoot}.IO";
        public const string ConfigurationErrorCode = $"{ValidationCodeRoot}.Configuration";
        public const string RequestErrorCode = $"{ValidationCodeRoot}.Request";
        public const string UnknownErrorCode = $"{ValidationCodeRoot}.Unknown";

        private static readonly KeyValuePair<LibraryIdentityValidatorType, string>[] libraryValidatorLevelToString = new[]
        {
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.Entity, EntityErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.Database, DatabaseErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.ExternalCommunication, ExternalCommunicationErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.IO, IOErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.Configuration, ConfigurationErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.Request, RequestErrorCode),
            new KeyValuePair<LibraryIdentityValidatorType, string>(LibraryIdentityValidatorType.Unknown, UnknownErrorCode),
        };

        public static readonly BidirectionalDictionary<LibraryIdentityValidatorType, string> ValidatorAndStringBidirectionalDict = new(libraryValidatorLevelToString);
        
        /// <summary>
        /// Constructs a valid Library Validation Error Code
        /// </summary>
        /// <param name="validationType">Type of validation that failed</param>
        /// <param name="specifiedErrorCode">End of error code that specifies what exactly went wrong.</param>
        /// <returns>A constructed error code following Domain rules.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="specifiedErrorCode"/> is null.</exception>
        public static ErrorCode ConstructErrorCode(LibraryIdentityValidatorType validationType, string specifiedErrorCode)
        {
            if (string.IsNullOrEmpty(specifiedErrorCode))
            {
                throw new ArgumentNullException(nameof(specifiedErrorCode), "Specified Error Code cannot be null or empty." +
                    "If you must construct an error code try calling `PrefixErrorCodeIfNoPrefixAttached()` to track LibraryValidatorType");
            }

            return PrefixErrorCodeIfNoPrefixAttached(validationType, specifiedErrorCode);
        }

        /// <summary>
        /// Attaches the Validation error prefix if there is no prefix attached. If <paramref name="errorCode"/> is null, its set to "NonSpecified"
        /// </summary>
        /// <param name="validationType">LType of validation that failed.</param>
        /// <param name="errorCode">Error code to prefix</param>
        /// <returns>A constructed error code following Domain rules.</returns>
        public static ErrorCode PrefixErrorCodeIfNoPrefixAttached(LibraryIdentityValidatorType validationType, string errorCode)
        {
            if (!string.IsNullOrEmpty(errorCode) && errorCode.StartsWith(ValidationCodeRoot))
            {
                return ErrorCode.ConstructFromStringRepresentation(errorCode);
            }

            if (string.IsNullOrEmpty(errorCode))
            {
                errorCode = "NonSpecific";    
            }

            if (ValidatorAndStringBidirectionalDict.TryGetByFirstKey(validationType, out string standardCode))
            {
                return ErrorCode.ConstructFromCombinedTypeDomain(standardCode, errorCode);
            }

            return ErrorCode.ConstructFromCombinedTypeDomain(UnknownErrorCode, errorCode);
        }

        public static LibraryIdentityValidatorType ValidationLevelFromCode(ErrorCode code)
        {
            string prefix = $"{code.ErrorType}.{code.ErrorDomain}";

            if (ValidatorAndStringBidirectionalDict.TryGetBySecondKey(prefix, out var validationLevel))
            {
                return validationLevel;
            }

            return LibraryIdentityValidatorType.Unknown;
        }
    }
}
