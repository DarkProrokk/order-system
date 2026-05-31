using Application.Interfaces;
using Application.Model;
using Domain.Exception;
using Domain.Result;

namespace Application.Services;

public class InventoryService(IItemRepository itemRepository): IInventoryService
{
    public async Task<Result<bool>> ReserveItem(ReserveItemModel model)
    {
        var item = await itemRepository.GetByIdAsync(model.Item.Id);
        if(item == null) return Result<bool>.Failure("Item not found");
        try
        {
            item.AdjustQuantity(model.Quantity);
        }
        catch (DomainException e)
        {
            return Result<bool>.Failure("insufficient_quantity");
        }
        return Result<bool>.Success(true);
    }
}