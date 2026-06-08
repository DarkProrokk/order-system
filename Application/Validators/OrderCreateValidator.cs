using Domain.Entity;
using Domain.Result;

namespace Application.Validators;

public class OrderCreateValidator
{
    public static Result<bool> Validate(Cart cart)
    {
        return cart.ValidateForOrder();
    }
}