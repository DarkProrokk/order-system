using Application.Extensions;
using Application.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace Application.Interfaces.Services;

public class ReservationExpirationService(IReservationRepository reservationRepository,
    IOrderRepository orderRepository,
    IUnitOfWork uow, ILogger<ReservationExpirationService> logger): IReservationExpirationService
{
    public async Task ProcessExpiredReservation()
    {
        Trace.StartActivity("ReservationExpirationService.ProcessExpiredReservation");
        logger.LogInformation("Starting expired reservation process");
        var expiredReservations = await reservationRepository.GetExpiredReservations();
        foreach (var expiredReservation in expiredReservations)
        {
            expiredReservation.Cancel();
            var order = await orderRepository.GetByIdAsync(expiredReservation.OrderId);
            order?.Cancel();
            reservationRepository.Update(expiredReservation);
            orderRepository.Update(order);
        }
        logger.LogInformation("Expired reservation process is done, processed {reservationCount} reservation", 
            expiredReservations.Count);
        await uow.SaveChangesAsync();
    }
}