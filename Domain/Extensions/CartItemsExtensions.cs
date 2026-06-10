using Domain.Entity;

namespace Domain.Extensions;

public static class CartItemsExtensions
{
    public static OrderItem MapToOrderItem(this CartItem cartItem)
    {
        return OrderItem.Create(cartItem.Item, cartItem.Quantity);
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