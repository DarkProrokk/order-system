using Application.Interfaces;
using Domain.Entity;
using Domain.Result;

namespace Application.Services;

public class OrderService(ICartRepository cartRepository): IOrderService
{
    public async Task<Result<string>> CreateOrder(int userId)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId);
        var reservation = new Reservation(cart.Items);
    }
}