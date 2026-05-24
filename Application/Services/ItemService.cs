using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ItemService(IItemRepository repository, ILogger<ItemService> logger): IItemService
{
    public void GenerateTestData(int count)
    {
        logger.LogInformation("Generating {count} items", count);
        repository.AddTestData(count);
    }
}