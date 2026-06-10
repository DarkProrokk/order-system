using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    public Task<Result> CreateOrder(int userId);
    public Task<Result> CancelOrder(int orderId);
}