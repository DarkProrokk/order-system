using Application.Extensions;
using Application.Interfaces;
using Domain.Entity;
using Domain.Exception;
using Domain.Result;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class CartRepository(OrderContext context, ILogger<CartRepository> logger): Repository<Cart>(context), ICartRepository
{
    public async Task<Result<bool>> AddItemInCartAsync(Item item, int cartId)
    {
        using var activity = Trace.StartActivity("CartRepository.AddItemInCart");
        Cart? currentCart = await Set.FindAsync(cartId);
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

    public async Task<Cart?> GetByUserIdAsync(int userId) => await Set.FirstOrDefaultAsync(c => c.User.Id == userId);
}