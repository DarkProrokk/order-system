using System.Diagnostics;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Model;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ItemRepository(OrderContext context): Repository<Item>(context), IItemRepository
{
    public async Task AddTestData(int count)
    {
        using var activity = Activity.Current?.Source.StartActivity("ItemRepository.AddTestData");
        var random = new Random();
        for (int i = 1; i <= count; i++)
        {
            var quantity = random.Next(1, 1000);
            decimal price = random.Next(1, 100);
            var point = (decimal)random.NextDouble();
            price += point;
            var item = new Item(price, $"Item {i}", quantity);
            await AddAsync(item);
        }
    }

    public async Task<List<Item>> GetFilteredItem(ItemFilterPagingModel model)
    {
        var query = Set.AsQueryable();
        query = query.Where(i => i.Quantity > 0);
        if (model.Filter != null)
        {
            if (model.Filter.PriceRange.MinPrice != null)
                query = query.Where(i => i.Price >= model.Filter.PriceRange.MinPrice);

            if (model.Filter.PriceRange.MaxPrice != null)
                query = query.Where(i => i.Price <= model.Filter.PriceRange.MaxPrice);
        }

        query = query.Take(model.Paging.PageSize).Skip((model.Paging.PageNumber - 1) * model.Paging.PageSize);
        return await query.ToListAsync();
    }
}