using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Result;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class OrderService(ICartRepository cartRepository, IUserRepository userRepository, IOrderRepository orderRepository, IReservationRepository reservationRepository,ILogger<OrderService> logger, IItemRepository itemRepository): IOrderService
{
    public async Task<Result<string>> CreateOrder(int userId)
    {
        Trace.StartActivity("OrderService.CreateOrder");
        logger.LogInformation("Starting create order");
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart == null)
        {
            logger.LogWarning("Cart not found for user {UserId}", userId);
            return Result<string>.Failure("Cart not found");
        }
        if (cart.Items.Count == 0)
        {
            logger.LogWarning("Empty cart for user {UserId}", userId);
            return Result<string>.Failure("Cart is empty");
        }
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            logger.LogError("user {userId} not found", userId);
            return Result<string>.Failure("User not found");
        }
        var order = new Order(cart.Items, user);
        var reservation = new Reservation(cart.Items, order);
        foreach (var item in cart.Items)
        {
            item.Item.AdjustQuantity(item.Quantity);
            itemRepository.Update(item.Item);
        }
        await reservationRepository.AddAsync(reservation);
        await orderRepository.AddAsync(order);
        await orderRepository.SaveChangesAsync();
        logger.LogInformation(
            "Order created for user {UserId}, items count {ItemsCount}",
            userId,
            cart.Items.Count);
        return Result<string>.Success("Order created");
    }
}