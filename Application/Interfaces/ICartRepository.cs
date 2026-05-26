using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces;

public interface ICartRepository: IRepository<Cart>
{
    public Result<bool> AddItemInCart(Item item, int cartId);
    public Cart? GetByUserId(int userId);
}