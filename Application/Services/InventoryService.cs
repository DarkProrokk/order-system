using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Model;
using Domain.Entity;
using Domain.Exception;
using Domain.Result;

namespace Application.Services;

public class InventoryService(IItemRepository itemRepository): IInventoryService
{
    public Result<bool> TryReserve(List<CartItem> cartItems)
    {
        Trace.StartActivity("InventoryService.Adjust");
        foreach (var cartItem in cartItems)
        {
            var result = cartItem.Item.AdjustQuantity(cartItem.Quantity);
            if (result.IsFailure) return Result<bool>.Failure(result.ErrorMessage);
        }
        return Result<bool>.Success();
    }
    // public Result<bool> TryReserve(List<CartItem> cartItems)
    // {
    //     if(!CanReserveAllItems(cartItems)) return Result<bool>.Failure("Can't reserve all items");
    //     return Reserve(cartItems);
    // }
    //
    // private bool CanReserveAllItems(List<CartItem> cartItems)
    // {
    //     foreach (var cartItem in cartItems)
    //     {
    //         if (!cartItem.Item.CanReserve(cartItem.Quantity)) return false;
    //     }
    //
    //     return true;
    // }
}