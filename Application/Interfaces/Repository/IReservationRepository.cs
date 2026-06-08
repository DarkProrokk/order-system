using Domain.Entity;

namespace Application.Interfaces.Repository;

public interface IReservationRepository: IRepository<Reservation>
{
    public Task<List<Reservation>> GetExpiredReservations();
}