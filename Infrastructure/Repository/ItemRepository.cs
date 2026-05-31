using System.Diagnostics;
using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ItemRepository(OrderContext context): Repository<Item>(context), IItemRepository
{
    public async Task AddTestData(int count)
    {
        using var activity = Activity.Current?.Source.StartActivity($"ItemRepository.AddTestData");
        var random = new Random();
        for (int i = 1; i <= count; i++)
        {
            decimal price = random.Next(1, 100);
            var point = (decimal)random.NextDouble();
            price += point;
            var item = new Item(price, $"Item {i}");
            await AddAsync(item);
        }
        await SaveChangesAsync();
    }
}