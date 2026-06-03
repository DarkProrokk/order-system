using Application.Interfaces;
using Application.Interfaces.Repository;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Di;

public static class RepositoryDiRegister
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    => services
        .AddScoped<IItemRepository, ItemRepository>()
        .AddScoped<ICartRepository, CartRepository>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IOrderRepository, OrderRepository>()
        .AddScoped<IReservationRepository, ReservationRepository>();
}