using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Result;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class OrderService(ICartRepository cartRepository, IUserRepository userRepository, 
    IOrderRepository orderRepository, 
    IReservationRepository reservationRepository,
    ILogger<OrderService> logger, IItemRepository itemRepository,
    IInventoryService inventoryService,
    IUnitOfWork uow): IOrderService
{
    public async Task<Result<string>> CreateOrder(int userId)
    {
        Trace.StartActivity("OrderService.CreateOrder");
        logger.LogInformation("Starting create order");
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            logger.LogWarning("user {userId} not found", userId);
            return Result<string>.Failure("User not found");
        }
        var cart = await cartRepository.GetByUserIdAsync(userId);
        if (cart == null)
        {
            logger.LogWarning("Cart not found for user {UserId}", userId);
            return Result<string>.Failure("Cart not found");
        }
        var validateResult = cart.ValidateForOrder();
        if (validateResult.IsFailure) return validateResult;
        
        var order = Order.CreateFrom(cart, user);
        var reservation = Reservation.CreateFrom(cart, order);
        
        var result = inventoryService.TryReserve(cart.CartItems);
        if (result.IsFailure) 
            return Result<string>.Failure(result.ErrorMessage);
        
        await reservationRepository.AddAsync(reservation);
        await orderRepository.AddAsync(order);
        await uow.SaveChangesAsync();
        logger.LogInformation(
            "Order created for user {UserId}, items count {ItemsCount}",
            userId,
            cart.CartItems.Count);
        return Result<string>.Success("Order created");
    }
}