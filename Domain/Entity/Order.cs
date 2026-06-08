using Domain.Enum;

namespace Domain.Entity;

public class Order: Entity
{
    public List<CartItem>? Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    
    public OrderStatus OrderStatus { get; private set; }
    
    private Order() {}

    public Order(List<CartItem> items, User user)
    {
        Items = items;
        User = user;
    }

    public void Cancel()
    {
        OrderStatus = OrderStatus.Canceled;
    }

    public static Order CreateFrom(Cart cart, User user)
    {
        return new Order(cart.CartItems, user);
    }
}