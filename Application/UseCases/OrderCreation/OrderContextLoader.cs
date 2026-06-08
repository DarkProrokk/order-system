using Application.Extensions;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Result;
using static Domain.Result.Result<Application.UseCases.OrderCreation.OrderContext>;
namespace Application.UseCases.OrderCreation;

public class OrderContextLoader(IUserRepository userRepository, ICartRepository cartRepository): IOrderContextLoader
{
    public async Task<Result<OrderContext>> Load(int userId)
    {
        using var activity = Trace.StartActivity("OrderContextLoader.Load");
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Failure("User not found");
        }
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart == null)
        {
            return Failure("Cart not found");
        }
        return Success(new OrderContext(user, cart));
    }
}