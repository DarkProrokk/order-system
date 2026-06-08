using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    public Task<Result<bool>> CreateOrder(int userId);
    public Task<Result<bool>> CancelOrder(int orderId);
}