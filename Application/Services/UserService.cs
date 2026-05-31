using Application.Interfaces;

namespace Application.Services;

public class UserService(IUserRepository repository): IUserService
{
    public async Task GenerateTestData(int count)
    {
        await repository.AddTestData(count);
    }
}