using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entity;
using Domain.Exception;
using Domain.Result;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class CartRepository(OrderContext context, ILogger<CartRepository> logger): Repository<Cart>(context), ICartRepository
{
    public async Task<Result<bool>> AddItemInCartAsync(Item item, Cart cart)
    {
        using var activity = Trace.StartActivity("CartRepository.AddItemInCart");
        try
        {
            cart.Add(item);
        }
        catch (DomainException e)
        {
            logger.LogError("Occured error while adding {item} in {cart}. Error: {error}", item, cart, e);
            return Result<bool>.Failure(e);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Cart?> GetByUserIdAsync(int userId) => await Set.Include(c => c.CartItems).ThenInclude(i => i.Item).FirstOrDefaultAsync(c => c.User.Id == userId);
}