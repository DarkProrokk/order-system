
using Application.Extensions;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ItemService(IItemRepository repository, ILogger<ItemService> logger): IItemService
{
    public void GenerateTestData(int count)
    {
        using var activity = Trace.StartActivity("ItemService.GenerateTestData");
        logger.LogInformation("Generating {count} items", count);
        Thread.Sleep(100);
        repository.AddTestData(count);
        //activity.Stop();
    }
}