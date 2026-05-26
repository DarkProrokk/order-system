using Application.Extensions;
using Application.Interfaces;
using Domain.Entity;
using Domain.Exception;
using Domain.Result;
using Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class CartRepository(OrderContext context, ILogger<CartRepository> logger): Repository<Cart>(context), ICartRepository
{
    public Result<bool> AddItemInCart(Item item, int cartId)
    {
        using var activity = Trace.StartActivity("CartRepository.AddItemInCart");
        Cart? currentCart = Set.Find(cartId);
        if (currentCart is null) return Result<bool>.Failure(new ArgumentException("Cart cannot be null in this context"));
        try
        {
            currentCart.Add(item);
        }
        catch (DomainException e)
        {
            logger.LogError("Occured error while adding {item} in {cart}. Error: {error}", item, currentCart, e);
            return Result<bool>.Failure(e);
        }

        return Result<bool>.Success(true);
    }

    public Cart? GetByUserId(int userId) => Set.FirstOrDefault(c => c.User.Id == userId);
}