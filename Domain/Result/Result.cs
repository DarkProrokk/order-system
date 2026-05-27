using Domain.Exception;

namespace Domain.Result;

public class Result<T>
{
    public T? Data { get; set; }
    
    public System.Exception? Error { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    
    private Result(bool isSuccess, T? data = default, System.Exception? error = default, string? errorMessage = default)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T data) => new Result<T>(true, data);
    public static Result<T> Failure(System.Exception error) => new Result<T>(true, default, error);
    public static Result<T> Failure(string errorMessage) => new Result<T>(true, default, null, errorMessage);
}