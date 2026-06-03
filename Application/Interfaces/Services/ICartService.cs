using Application.Model;
using Domain.Result;

namespace Application.Interfaces.Services;

public interface ICartService
{
    public Task<Result<bool>> AddItemAsync(AddItemInCartModel model);
}