using Application.Model;
using Domain.Entity;
using Domain.Result;

namespace Application.Interfaces;

public interface ICartService
{
    public Task<Result<bool>> AddItemAsync(AddItemInCartModel model);
}