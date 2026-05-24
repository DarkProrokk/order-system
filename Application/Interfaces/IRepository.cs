using Domain.Entity;

namespace Application.Interfaces;

public interface IRepository<T>  where T : Entity
{
    public void Add(T entity);
    public T? GetById(int id);
    public void Update(T entity);
    public void SaveChanges();
}
    