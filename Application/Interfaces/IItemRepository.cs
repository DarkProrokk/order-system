using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces;

public interface IItemRepository: IRepository<Item>
{
    public Task AddTestData(int count);
}