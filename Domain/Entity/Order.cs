using Domain.Enum;
using static Domain.Result.Result;

namespace Domain.Entity;

public class Order: Entity
{
    public List<OrderItem>? Items { get; set; } = new List<OrderItem>();
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    
    public OrderStatus Status { get; private set; }
    
    private Order() {}

    private Order(List<OrderItem> items, User user)
    {
        Items = items;
        User = user;
        Status = OrderStatus.Created;
    }
    
    private Order(OrderItem item, User user)
    {
        Items  = new List<OrderItem> {item};
        User = user;
        Status = OrderStatus.Created;
    }

    public void Cancel()
    {
        ChangeStatus(OrderStatus.Canceled);
    }

    public static Order CreateFrom(List<OrderItem> orderItems, User user)
    {
        return new Order(orderItems, user);
    }
    
    public static Order CreateFrom(OrderItem orderItem, User user)
    {
        return new Order(orderItem, user);
    }

    public Result.Result ChangeStatus(OrderStatus newStatus)
    {
        if (!CanTransitionTo(newStatus)) return Failure($"Cannot change status {Status} to {newStatus}");
        Status = newStatus;
        return Success();
    }


    private bool CanTransitionTo(OrderStatus newStatus)
    {
        return Status switch
        {
            OrderStatus.Created => newStatus is OrderStatus.Paid or OrderStatus.Canceled,
            OrderStatus.Paid => newStatus is OrderStatus.Delivered or OrderStatus.Canceled,
            _ => false
        };
    }
}