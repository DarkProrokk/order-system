using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Moq;

namespace Tests.Application.Service;

public class OrderServiceTests
{
    [Fact]
    public void OrderShouldBeCreated()
    {
        var repo = new Mock<IOrderService>();
        repo.Setup(x => x.CreateOrder());
    }
}