using Domain.Entity;

namespace Domain.Extensions;

public static class OrderItemExtensions
{
    public static ReservationItem MapToOrderItem(this OrderItem orderItem)
    {
        return ReservationItem.Create(orderItem.Item, orderItem.Quantity);
    }
    
    public static List<OrderItem> MapToOrderItem(this List<CartItem> cartItems)
    {
        List<OrderItem> orderItems = new List<OrderItem>();

        foreach (var cartItem in cartItems)
        {
            orderItems.Add(cartItem.MapToOrderItem());
        }

        return orderItems;
    }

}