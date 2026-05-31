using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class Repository<T>(OrderContext context): IRepository<T> where T : Entity
{
    internal readonly DbSet<T> Set = context.Set<T>();
    public async Task AddAsync(T entity)
    {
        await Set.AddAsync(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await  Set.FindAsync(id);
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }

    public void Delete(T entity)
    {
        Set.Remove(entity);
    }

    public async Task SoftDeleteAsync(T entityToDelete)
    {
       var entity = await Set.FindAsync(entityToDelete.Id);
       entity?.IsDeleted = true;
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}