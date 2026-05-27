using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace Application.Di;

public static class ApplicationDiRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
        => service
            .AddScoped<IItemService, ItemService>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<IUserService, UserService>();
}