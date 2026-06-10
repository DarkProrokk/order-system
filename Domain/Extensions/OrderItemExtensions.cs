using Domain.Entity;

namespace Domain.Extensions;

public static class OrderItemExtensions
{
    public static ReservationItem MapToReservationItem(this OrderItem orderItem)
    {
        return ReservationItem.Create(orderItem.ItemId, orderItem.Quantity);
    }
    
    public static List<ReservationItem> MapToReservationItem(this List<OrderItem> orderItems)
    {
        List<ReservationItem> reservationItems = new List<ReservationItem>();

        foreach (var orderItem in orderItems)
        {
            reservationItems.Add(orderItem.MapToReservationItem());
        }

        return reservationItems;
    }


    public static CartItem MapToCartItem(this OrderItem orderItem, Cart cart)
    {
        return CartItem.Create(cart, orderItem.ItemId, orderItem.Quantity);
    }
    
    
    public static List<CartItem> MapToCartItem(this List<OrderItem> orderItems, Cart cart)
    {
        List<CartItem> cartItems = new List<CartItem>();

        foreach (var orderItem in orderItems)
        {
            cartItems.Add(orderItem.MapToCartItem(cart));
        }

        return cartItems;
    }

}