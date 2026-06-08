using Application.UseCases.OrderCreation;
using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IOrderContextLoader
{
    Task<Result<OrderContext>> Load(int  userId);
}