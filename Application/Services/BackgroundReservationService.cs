using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class BackgroundReservationService: BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}