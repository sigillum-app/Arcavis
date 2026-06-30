namespace Sigillum.Arcavis.Core.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public IReadOnlyList<IError> Errors { get; }

    private protected Result(bool success, IEnumerable<IError>? errors = null)
    {
        var errorList = errors?.ToArray() ?? [];

        if (success && errorList.Length != 0)
            throw new InvalidOperationException("A successful result cannot contain errors.");

        if (!success && errorList.Length == 0)
            throw new InvalidOperationException("A failed result must contain at least one error.");

        IsSuccess = success;
        Errors = errorList;
    }

    public static Result Success()
        => new(true);

    public static Result Failure(params IError[] errors)
        => new(false, errors);

    public static Result Failure(IEnumerable<IError> errors)
        => new(false, errors);
}

public sealed class Result<T> : Result
{
    public T? Value { get; }

    private protected Result(bool success, T? value = default, IEnumerable<IError>? errors = null) : base(success, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
        => new(true, value);

    public static new Result<T> Failure(params IError[] errors)
        => new(false, default, errors);

    public static Result<T> Failure(IEnumerable<IError> errors)
        => new(false, default, errors);
}
