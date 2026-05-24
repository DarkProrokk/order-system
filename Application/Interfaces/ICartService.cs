using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces;

public interface ICartService
{
    public Result<bool> AddItem(Item item, User user);
}