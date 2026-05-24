using Domain.Exception;

namespace Domain.Result;

public class Result<T>
{
    public T? Data { get; set; }
    
    public DomainException? Error { get; set; }
    
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    
    private Result(bool isSuccess, T? data = default, DomainException? error = default)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new Result<T>(true, data);
    public static Result<T> Failure(DomainException error) => new Result<T>(true, default, error);
}