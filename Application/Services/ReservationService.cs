using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Result;

namespace Application.Services;

public class ReservationService: IReservationService
{
    public Task<Result<bool>> CancelExpiredReservation()
    {
        throw new NotImplementedException();
    }
}