using Domain.Entity;

namespace Application.Interfaces;

public interface IItemRepository: IRepository<Item>
{
    public void AddTestData(int count);
}