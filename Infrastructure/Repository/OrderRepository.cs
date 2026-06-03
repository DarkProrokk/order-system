using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entity;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class OrderRepository(OrderContext context) : Repository<Order>(context), IOrderRepository
{
    
}