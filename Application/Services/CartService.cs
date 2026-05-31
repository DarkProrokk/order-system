using System.Diagnostics;
using Application.Interfaces;
using Application.Model;
using Domain.Entity;
using Domain.Result;
using Trace = Application.Extensions.Trace;

namespace Application.Services;

public class CartService(IItemRepository itemRepository, ICartRepository cartRepository, IUserRepository userRepository): ICartService

{
    public async Task<Result<bool>> AddItemAsync(AddItemInCartModel model)
    {
        using var activity = Trace.StartActivity("CartService.AddItem");
        Cart? cart;
        cart = await cartRepository.GetByUserIdAsync(model.UserId);
        if (cart is null)
        {
            cart = new Cart
            {
                UserId = model.UserId
            };
            await cartRepository.AddAsync(cart);
            await cartRepository.SaveChangesAsync();
        }

        var item = await itemRepository.GetByIdAsync(model.ItemId);
        if (item == null) return Result<bool>.Failure("Item not found");
        var result = await cartRepository.AddItemInCartAsync(item, cart.Id);
        if (result.IsSuccess)
        {
            cartRepository.Update(cart);
            await cartRepository.SaveChangesAsync();
        }

        return result;
    }
}