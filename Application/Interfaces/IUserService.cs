namespace Application.Interfaces;

public interface IUserService
{
    public Task GenerateTestData(int count);
}