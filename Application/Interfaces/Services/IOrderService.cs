using Domain.Result;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    public Task<Result<string>> CreateOrder(int userId);
}