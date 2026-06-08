using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IReservationService
{
    public Task<Result<Reservation>> ReserveAsync(Order order);
    public Task<Result<bool>> CancelExpiredReservation();
    // public Task<Result<bool>> GetExpiredAsync();
}