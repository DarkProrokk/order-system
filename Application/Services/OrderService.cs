using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Validators;
using Domain.Entity;
using Domain.Extensions;
using Domain.Result;
using Microsoft.Extensions.Logging;
using static Domain.Result.Result;

namespace Application.Services;

public class OrderService(IOrderContextLoader contextLoader, 
    IOrderRepository orderRepository, 
    IReservationService reservationService,
    ILogger<OrderService> logger,
    IUnitOfWork uow): IOrderService
{
    public async Task<Result> CreateOrder(int userId)
    {
        using var activity = Trace.StartActivity("OrderService.CreateOrder");
        logger.LogInformation("Starting create order");
        var orderContext = await contextLoader.Load(userId);
        
        if (orderContext.IsFailure) 
            return Failure(orderContext.ErrorMessage); 
        
        var validateResult = OrderCreateValidator.Validate(orderContext.Value!.cart);
        if (validateResult.IsFailure) 
            return validateResult;
        
        var order = Order.CreateFrom(orderContext.Value.cart.CartItems.MapToOrderItem(), orderContext.Value.user);
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

    public async Task<Result> CancelOrder(int orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if(order is null) return Failure($"Order - {orderId} not found");

        var cancelReservationResult = await reservationService.CancelReservationByOrderId(orderId);
        if (cancelReservationResult.IsFailure) return cancelReservationResult;
        
        return Success();
    }
}