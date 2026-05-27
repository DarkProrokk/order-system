using Domain.Entity;

namespace Application.Interfaces;

public interface IUserRepository: IRepository<User>
{
    public void AddTestData(int count);
}