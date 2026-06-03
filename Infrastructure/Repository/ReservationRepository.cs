using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entity;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ReservationRepository(OrderContext context) : Repository<Reservation>(context), IReservationRepository
{
    
}