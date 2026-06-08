using Application.Model;
using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IInventoryService
{
    public Result<bool> TryReserve(List<CartItem> items);
}