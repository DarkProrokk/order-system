using Domain.Enum;

namespace Domain.Entity;

public class Reservation: Entity
{
    public List<CartItem> ReservedItem { get; set; }
    public DateTime CratedAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    public Order Order { get; set; }
    
    public int OrderId { get; set; }
    
    private Reservation()
    {
        
    }
    private Reservation(List<CartItem> reservedItem, Order order)
    {
        ReservedItem = reservedItem;
        CratedAt = DateTime.UtcNow;
        Status = ReservationStatus.Reserved;
        Order = order;
    }

    public void Cancel()
    {
        Status = ReservationStatus.Canceled;
    }

    public static Reservation CreateFrom(Cart cart, Order order)
    {
        return new Reservation(cart.CartItems, order);
    }
}