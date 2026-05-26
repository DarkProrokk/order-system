using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class Repository<T>(OrderContext context): IRepository<T> where T : Entity
{
    internal readonly DbSet<T> Set = context.Set<T>();
    public void Add(T entity)
    {
        Set.Add(entity);
    }

    public T? GetById(int id)
    {
        return Set.Find(id);
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }

    public void Delete(T entity)
    {
        Set.Remove(entity);
    }

    public void SoftDelete(T entityToDelete)
    {
       var entity = Set.Find(entityToDelete.Id);
       entity?.IsDeleted = true;
    }
    
    public void SaveChanges()
    {
        context.SaveChanges();
    }
}