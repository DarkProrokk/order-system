using Domain.Entity;

namespace Application.Interfaces;

public interface IUserRepository: IRepository<User>
{
    public Task AddTestData(int count);
}