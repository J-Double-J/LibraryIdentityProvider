﻿using Newtonsoft.Json;

namespace LibraryIdentityProvider.Patterns.ResultAndError
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; private init; }

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        public Error? Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(TValue? value, Error error) => new(value, false, error);
    }
}
