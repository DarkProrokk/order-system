using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface IItemService
{
    public Task GenerateTestData(int count);

    public Task<Result<List<Item>>> GetAll();
}