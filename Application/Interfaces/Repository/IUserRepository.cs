using Domain.Entity;

namespace Application.Interfaces.Repository;

public interface IUserRepository: IRepository<User>
{
    public Task AddTestData(int count);
}