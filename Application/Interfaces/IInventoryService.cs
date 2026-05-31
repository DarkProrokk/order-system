using Application.Model;
using Domain.Result;

namespace Application.Interfaces;

public interface IInventoryService
{
    public Task<Result<bool>> ReserveItem(ReserveItemModel model);
}