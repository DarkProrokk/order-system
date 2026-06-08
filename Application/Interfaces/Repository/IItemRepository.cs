using Application.Model;
using Domain.Entity;

namespace Application.Interfaces.Repository;

public interface IItemRepository: IRepository<Item>
{
    public Task AddTestData(int count);
    
    public Task<List<Item>> GetFilteredItem(ItemFilterPagingModel model);
}