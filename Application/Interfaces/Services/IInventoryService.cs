using Application.Model;
using Application.UseCases;
using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IInventoryService
{
    public Task<Result<List<AdjustmentItem>>> TryReserve(List<OrderItem> orderItems);
}