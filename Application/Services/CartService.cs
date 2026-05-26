using Application.Interfaces;
using Domain.Entity;
using Domain.Result;

namespace Application.Services;

public class CartService(IItemRepository itemRepository, ICartRepository cartRepository): ICartService

{
    public Result<bool> AddItem(Item item, User user)
    {
        Cart cart;
        cart = cartRepository.GetByUserId(user.Id);
        if (cart is null)
        {
            cart = new Cart()
            {
                User = user
            };
            cartRepository.Add(cart);
            cartRepository.SaveChanges();
        }

        
        return cartRepository.AddItemInCart(item, cart.Id);
    }
}