using Domain.Enum;
using static Domain.Result.Result;

namespace Domain.Entity;

public class Order: Entity
{
    public List<OrderItem>? Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    
    public OrderStatus Status { get; private set; }
    
    private Order() {}

    public Order(List<OrderItem> items, User user)
    {
        Items = items;
        User = user;
    }

    public void Cancel()
    {
        ChangeStatus(OrderStatus.Canceled);
    }

    public static Order CreateFrom(List<OrderItem> orderItems, User user)
    {
        return new Order(orderItems, user);
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