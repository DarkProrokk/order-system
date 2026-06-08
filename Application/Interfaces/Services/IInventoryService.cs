using Application.Model;
using Application.UseCases;
using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IInventoryService
{
    public Result<List<AdjustmentItem>> TryReserve(List<CartItem> items);
}