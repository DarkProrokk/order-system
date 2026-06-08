using Application.Extensions;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Result;

namespace Application.Services;

public class ReservationService(IInventoryService inventoryService, IReservationRepository reservationRepository, IItemRepository itemRepository): IReservationService
{
    public async Task<Result<Reservation>> ReserveAsync(Order order)
    {
        using var activity = Trace.StartActivity("ReservationService.ReserveAsync");
        var itemReserveResult = inventoryService.TryReserve(order.Items);
        if (itemReserveResult.IsFailure) return Result<Reservation>.Failure(itemReserveResult.ErrorMessage);
        foreach (var adjustmentItem in itemReserveResult.Value!)
        {
            var item = await itemRepository.GetByIdAsync(adjustmentItem.itemId);
            item.ReduceStock(adjustmentItem.quantity);
        }
        var reservation = Reservation.CreateFrom(order);
        await reservationRepository.AddAsync(reservation);
        return Result<Reservation>.Success(reservation);

    }

    public Task<Result<bool>> CancelExpiredReservation()
    {
        throw new NotImplementedException();
    }
}