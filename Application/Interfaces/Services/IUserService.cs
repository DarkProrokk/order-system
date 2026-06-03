namespace Application.Interfaces.Services;

public interface IUserService
{
    public Task GenerateTestData(int count);
}