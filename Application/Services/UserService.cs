using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;

namespace Application.Services;

public class UserService(IUserRepository repository): IUserService
{
    public async Task GenerateTestData(int count)
    {
        await repository.AddTestData(count);
    }
}