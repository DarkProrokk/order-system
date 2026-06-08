using Domain.Entity;

namespace Application.Interfaces.Services;

public interface IReservationExpirationService
{
    public Task ProcessExpiredReservation();
}