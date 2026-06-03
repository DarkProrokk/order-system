using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Repository;

public interface ICartRepository: IRepository<Cart>
{
    public Task<Result<bool>> AddItemInCartAsync(Item item, Cart cart);
    public Task<Cart?> GetByUserIdAsync(int userId);
}