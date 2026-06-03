using Domain.Entity;

namespace Application.Interfaces.Repository;

public interface IItemRepository: IRepository<Item>
{
    public Task AddTestData(int count);
}