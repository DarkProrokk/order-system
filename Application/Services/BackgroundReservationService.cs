using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class BackgroundReservationService(IServiceScopeFactory scopeFactory): BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            // Console.WriteLine("123123123");
            // var scope = scopeFactory.CreateScope();
            // IReservationService reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
            // var expiredReservations = await reservationService.GetExpired();
            // foreach (var reservation in expiredReservations.Data)
            // {
            //     await reservationService.Cancel(reservation);
            // }
            //
            //
            // await Task.Delay(TimeSpan.FromSeconds(20));
        }
    }
}