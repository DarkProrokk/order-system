using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;

namespace Application.Services;

public class UserService(IUserRepository repository, IUnitOfWork uow): IUserService
{
    public async Task GenerateTestData(int count)
    {
        await repository.AddTestData(count);
        await uow.SaveChangesAsync();
    }
}