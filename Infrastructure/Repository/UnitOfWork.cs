using Application.Extensions;
using Application.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class UnitOfWork(OrderContext context): IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        Trace.StartActivity("UnitOfWork.SaveChangesAsync");
        await context.SaveChangesAsync();
    }
}