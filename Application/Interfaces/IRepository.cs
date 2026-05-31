using Domain.Entity;

namespace Application.Interfaces;

public interface IRepository<T>  where T : Entity
{
    public Task AddAsync(T entity);
    public Task<T?> GetByIdAsync(int id);
    public void Update(T entity);
    public Task SaveChangesAsync();
    
    public void Delete(T entity);
    public Task SoftDeleteAsync(T entity);
}
    