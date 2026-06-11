using Domain.Entity;
using Domain.Enum;

namespace Tests.Domain.Entity;

public class OrderTests
{
    private static User CreateUser()
    {
        var user =  User.Create("test@test.com");
        user.Id = 1;
        return user;
    }

    private static OrderItem CreateOrderItem()
    {
        var item = new Item(55, "Item1", 20);
        return OrderItem.Create(item, 5);
    }

    private static Order CreateOrder()
    {
        var item = CreateOrderItem();
        var user = CreateUser();
        return Order.CreateFrom(item, user);
    }

    public static IEnumerable<object[]> StatusTransitions =>
    [
        // from Created
        [OrderStatus.Created, OrderStatus.Paid, true],
        [OrderStatus.Created, OrderStatus.Canceled, true],
        [OrderStatus.Created, OrderStatus.Delivered, false],

        // from Paid
        [OrderStatus.Paid, OrderStatus.Delivered, true],
        [OrderStatus.Paid, OrderStatus.Canceled, true],
        [OrderStatus.Paid, OrderStatus.Created, false],

        // from Delivered
        [OrderStatus.Delivered, OrderStatus.Paid, false],
        [OrderStatus.Delivered, OrderStatus.Canceled, false],
        [OrderStatus.Delivered, OrderStatus.Created, false],

        // from Canceled
        [OrderStatus.Canceled, OrderStatus.Paid, false],
        [OrderStatus.Canceled, OrderStatus.Delivered, false],
        [OrderStatus.Canceled, OrderStatus.Created, false],
    ];

    [Theory]
    [MemberData(nameof(StatusTransitions))]
    public void ChangeStatus_Should_Respect_StateMachine(
        OrderStatus initialStatus,
        OrderStatus targetStatus,
        bool shouldSucceed)
    {
        // Arrange
        var order = CreateOrder();
        SetStatus(order, initialStatus);

        var expectedStatus = shouldSucceed ? targetStatus : initialStatus;

        // Act
        var result = order.ChangeStatus(targetStatus);

        // Assert
        Assert.Equal(shouldSucceed, result.IsSuccess);
        Assert.Equal(expectedStatus, order.Status);
    }

    [Fact]
    public void CreateFrom_SingleItem_Should_Create_Order()
    {
        var order = Order.CreateFrom(CreateOrderItem(), CreateUser());

        Assert.NotNull(order);
        Assert.Single(order.Items);
        Assert.Equal(OrderStatus.Created, order.Status);
    }

    [Fact]
    public void CreateFrom_List_Should_Create_Order()
    {
        var items = new List<OrderItem>
        {
            CreateOrderItem(),
            CreateOrderItem()
        };

        var order = Order.CreateFrom(items, CreateUser());

        Assert.Equal(2, order.Items.Count);
        Assert.Equal(OrderStatus.Created, order.Status);
    }

    [Fact]
    public void Cancel_Should_Set_Status_To_Canceled()
    {
        var order = CreateOrder();

        order.Cancel();

        Assert.Equal(OrderStatus.Canceled, order.Status);
    }

    private static void SetStatus(Order order, OrderStatus status)
    {
        if (status == OrderStatus.Created)
            return;

        if (status == OrderStatus.Paid)
        {
            order.ChangeStatus(OrderStatus.Paid);
            return;
        }

        if (status == OrderStatus.Delivered)
        {
            order.ChangeStatus(OrderStatus.Paid);
            order.ChangeStatus(OrderStatus.Delivered);
            return;
        }

        if (status == OrderStatus.Canceled)
        {
            order.ChangeStatus(OrderStatus.Canceled);
        }
    }
}