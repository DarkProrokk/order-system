using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repository;

namespace Infrastructure.Di;

public static class InfrastructureDiRegister
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddDbContext<OrderContext>(options => options.UseNpgsql(configuration.GetConnectionString("OrderContext")))
            .AddRepository();
}