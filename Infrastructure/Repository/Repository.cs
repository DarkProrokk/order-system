using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class Repository<T>(OrderContext context): IRepository<T> where T : Entity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public T? GetById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void SoftDelete(T entityToDelete)
    {
       var entity = context.Set<T>().Find(entityToDelete.Id);
       entity?.IsDeleted = true;
    }
    
    public void SaveChanges()
    {
        context.SaveChanges();
    }
}