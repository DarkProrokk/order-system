using Domain.Exception;

namespace Domain.Result;

public class Result<T>: Result
{
    public T? Value { get; }
    
    protected Result(bool isSuccess, T? data = default, string? errorMessage = default) : base(isSuccess, errorMessage)
    {
        Value = data;
    }

    public static Result<T> Success(T? data = default) => new(true, data);
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
    public static Result<T> Failure(T data, string errorMessage) => new(false, data, errorMessage);
}

public class Result
{
    public string? ErrorMessage { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    
    protected Result(bool isSuccess, string? errorMessage = default)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
    
    public static Result Success() => new(true);
    public static Result Failure(string? errorMessage) => new(false, errorMessage);
}