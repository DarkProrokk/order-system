using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces;

public interface ICartRepository: IRepository<Cart>
{
    public Task<Result<bool>> AddItemInCartAsync(Item item, int cartId);
    public Task<Cart?> GetByUserIdAsync(int userId);
}