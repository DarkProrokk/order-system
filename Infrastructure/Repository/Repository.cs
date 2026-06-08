using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class Repository<T>(OrderContext context): IRepository<T> where T : Entity
{
    internal readonly DbSet<T> Set = context.Set<T>();
    public async Task AddAsync(T entity)
    {
        Trace.StartActivity($"{GetType().Namespace}.AddAsync");
        await Set.AddAsync(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        Trace.StartActivity($"{GetType().Namespace}.GetByIdAsync");
        return await  Set.FindAsync(id);
    }

    public void Update(T? entity)
    {
        Trace.StartActivity($"{GetType().Namespace}.Update");
        if (entity == null) return;
        Set.Update(entity);
    }

    public void UpdateRange(IEnumerable<T>? entities)
    {
        Trace.StartActivity($"{GetType().Namespace}.UpdateRange");
        if (entities == null) return;
        Set.UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        Trace.StartActivity($"{GetType().Namespace}.Delete");
        Set.Remove(entity);
    }

    public async Task SoftDeleteAsync(T entityToDelete)
    {
        Trace.StartActivity($"{GetType().Namespace}.SoftDeleteAsync");
       var entity = await Set.FindAsync(entityToDelete.Id);
       entity?.IsDeleted = true;
    }
}