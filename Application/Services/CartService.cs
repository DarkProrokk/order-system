using System.Diagnostics;
using Application.Interfaces;
using Application.Model;
using Domain.Entity;
using Domain.Result;
using Trace = Application.Extensions.Trace;

namespace Application.Services;

public class CartService(IItemRepository itemRepository, ICartRepository cartRepository, IUserRepository userRepository): ICartService

{
    public Result<bool> AddItem(AddItemInCartModel model)
    {
        using var activity = Trace.StartActivity("CartService.AddItem");
        Cart? cart;
        cart = cartRepository.GetByUserId(model.UserId);
        if (cart is null)
        {
            cart = new Cart
            {
                UserId = model.UserId
            };
            cartRepository.Add(cart);
            cartRepository.SaveChanges();
        }

        var item = itemRepository.GetById(model.ItemId);
        if (item == null) return Result<bool>.Failure("Item not found");
        var result =cartRepository.AddItemInCart(item, cart.Id);
        if (result.IsSuccess)
        {
            cartRepository.Update(cart);
            cartRepository.SaveChanges();
        }

        return result;
    }
}