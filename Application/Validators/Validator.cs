using Domain.Result;

namespace Application.Validators;

public abstract class Validator
{
    public abstract Task<Result<bool>> Validate();
}