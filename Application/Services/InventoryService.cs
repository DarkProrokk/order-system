using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Model;
using Application.UseCases;
using Domain.Entity;
using Domain.Exception;
using Domain.Result;
using static Domain.Result.Result<System.Collections.Generic.List<Application.UseCases.AdjustmentItem>>;

namespace Application.Services;

public class InventoryService(IItemRepository itemRepository): IInventoryService
{
    public Result<List<AdjustmentItem>> TryReserve(List<CartItem> cartItems)
    {
        using var activity = Trace.StartActivity("InventoryService.Adjust");
        var adjustmentItems = new List<AdjustmentItem>();
        var outOfStockItems = new List<AdjustmentItem>();
        foreach (var cartItem in cartItems)
        {
            var item = cartItem.Item;
            var tempItem = new AdjustmentItem(item.Id, item.Quantity);
            if (item.Quantity < cartItem.Quantity)
            {
                outOfStockItems.Add(tempItem);
            }
            else
            {
                adjustmentItems.Add(tempItem);     
            }
            
            // var result = cartItem.Item.AdjustQuantity(cartItem.Quantity);
            // if (result.IsFailure) return Result<bool>.Failure(result.ErrorMessage);
        }
        if(outOfStockItems.Any()) return Failure(outOfStockItems, "Some items out of stock");
        return Success(adjustmentItems);
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