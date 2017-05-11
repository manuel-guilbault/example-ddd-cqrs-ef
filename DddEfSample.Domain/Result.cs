using System;

namespace DddEfSample.Domain
{
    public class Result: IEquatable<Result>
    {
        public static Result Success() => new Result(true);
        public static Result Failure() => new Result(false);
        public static Result<TError> Success<TError>() => Result<TError>.Success();
        public static Result<TError> Failure<TError>(TError error) => Result<TError>.Failure(error);
        public static Result<TValue, TError> Success<TValue, TError>(TValue value) => Result<TValue, TError>.Success(value);
        public static Result<TValue, TError> Failure<TValue, TError>(TError error) => Result<TValue, TError>.Failure(error);

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Result);
        }

        public bool Equals(Result other)
        {
            return !ReferenceEquals(other, null) && IsSuccess == other.IsSuccess;
        }
    }

    public class Result<TError>: Result, IEquatable<Result<TError>>
    {
        public new static Result<TError> Success() => new Result<TError>(true, default(TError));

        public static Result<TError> Failure(TError error) => new Result<TError>(false, error);

        protected Result(bool isSuccess, TError error)
            : base(isSuccess)
        {
            Error = error;
        }

        public TError Error { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Result<TError>);
        }

        public bool Equals(Result<TError> other)
        {
            return base.Equals(other) && Equals(Error, other.Error);
        }
    }

    public class Result<TValue, TError>: Result<TError>, IEquatable<Result<TValue, TError>>
    {
        public static Result<TValue, TError> Success(TValue value) => new Result<TValue, TError>(true, value, default(TError));
        public new static Result<TValue, TError> Failure(TError error) => new Result<TValue, TError>(false, default(TValue), error);

        private Result(bool isSuccess, TValue value, TError error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public TValue Value { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Result<TValue, TError>);
        }

        public bool Equals(Result<TValue, TError> other)
        {
            return base.Equals(other) && Equals(Value, other.Value);
        }
    }
}
