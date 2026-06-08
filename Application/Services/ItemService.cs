
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entity;
using Domain.Result;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ItemService(IItemRepository repository, ILogger<ItemService> logger, IUnitOfWork uow): IItemService
{
    public async Task GenerateTestData(int count)
    {
        using var activity = Trace.StartActivity("ItemService.GenerateTestData");
        logger.LogInformation("Generating {count} items", count);
        Thread.Sleep(100);
        await repository.AddTestData(count);
        await uow.SaveChangesAsync();
        //activity.Stop();
    }

    public Task<Result<List<Item>>> GetAll()
    {
        
    }
}