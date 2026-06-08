using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Validators;
using Domain.Entity;
using Domain.Result;
using Microsoft.Extensions.Logging;
using static Domain.Result.Result<bool>;

namespace Application.Services;

public class OrderService(IOrderContextLoader contextLoader, 
    IOrderRepository orderRepository, 
    IReservationService reservationService,
    ILogger<OrderService> logger,
    IUnitOfWork uow): IOrderService
{
    public async Task<Result<bool>> CreateOrder(int userId)
    {
        using var activity = Trace.StartActivity("OrderService.CreateOrder");
        logger.LogInformation("Starting create order");
        var orderContext = await contextLoader.Load(userId);
        
        if (orderContext.IsFailure) 
            return Failure(orderContext.ErrorMessage); 
        
        var validateResult = OrderCreateValidator.Validate(orderContext.Value!.cart);
        if (validateResult.IsFailure) 
            return validateResult;
        
        var order = Order.CreateFrom(orderContext.Value.cart, orderContext.Value.user);
        var reservationResult = await reservationService.ReserveAsync(order);
        
        if (reservationResult.IsFailure) 
            return Failure(reservationResult.ErrorMessage);
        orderContext.Value.cart.CartItems.Clear();
        await orderRepository.AddAsync(order);
        await uow.SaveChangesAsync();
        
        logger.LogInformation(
            "Order created for user {UserId}, items count {ItemsCount}",
            userId,
            orderContext.Value.cart.CartItems.Count);
        return Success();
    }

    public Task<Result<bool>> CancelOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}