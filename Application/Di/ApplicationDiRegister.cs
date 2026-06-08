using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.UseCases.OrderCreation;

namespace Application.Di;

public static class ApplicationDiRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
        => service
            .AddScoped<IItemService, ItemService>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IReservationService, ReservationService>()
            .AddScoped<IInventoryService, InventoryService>()
            .AddScoped<IOrderContextLoader, OrderContextLoader>()
            .AddHostedService<BackgroundReservationService>();
}