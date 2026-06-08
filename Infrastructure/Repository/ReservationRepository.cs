
using Application.Extensions;
using Application.Interfaces.Repository;
using Domain.Entity;
using Domain.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ReservationRepository(OrderContext context) : Repository<Reservation>(context), IReservationRepository
{
    public async Task<List<Reservation>> GetExpiredReservations()
    {
        Trace.StartActivity("ReservatinRepository.GetExpiredReservations");
        return await Set.Where(r =>
            r.Status == ReservationStatus.Reserved && r.CratedAt <= DateTime.UtcNow - TimeSpan.FromMinutes(30)).ToListAsync();
    }
}