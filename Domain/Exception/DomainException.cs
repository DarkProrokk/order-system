namespace Domain.Exception;

public class DomainException: System.Exception
{
    public string Code { get; }

    public DomainException(string code, string message) : base(message)
    {
        Code = code;
    }
}