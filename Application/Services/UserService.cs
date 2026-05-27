using Application.Interfaces;

namespace Application.Services;

public class UserService(IUserRepository repository): IUserService
{
    public void GenerateTestData()
    {
        repository.AddTestData(100);
    }
}