using Domain.Result;

namespace Application.Interfaces.Services;

public interface IReservationService
{
    public Task<Result<bool>> CancelExpiredReservation();
}