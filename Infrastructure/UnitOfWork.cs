using Application.Interfaces;
using Infrastructure.Context;

namespace Infrastructure;

public class UnitOfWork(OrderContext context): IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}