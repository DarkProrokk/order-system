
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
        // int threads = Environment.ProcessorCount;
        //
        // for (int i = 0; i < threads; i++)
        // {
        //     new Thread(HeavyWork).Start();
        // }
        Thread.Sleep(100);
        repository.AddTestData(count);
        //activity.Stop();
    }
    
    static void HeavyWork()
    {
        double x = 0;

        for(long i = 0; i <= 10000000000; i++)
        {
            x += Math.Sqrt(x + 1.000001);
            x *= 1.0000001;

            // предотвращаем оптимизацию "в ноль"
            if (x > 1e100)
                x = 1;
                
        }
    }
}