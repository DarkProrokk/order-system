using Domain.Result;

namespace Application.Interfaces;

public interface IOrderService
{
    public Task<Result<string>> CreateOrder(int userId);
}