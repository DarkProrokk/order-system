using Domain.Enum;

namespace Domain.Entity;

public class Reservation: Entity
{
    public List<CartItem> ReservedItem { get; set; }
    public DateTime CratedAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    public Order Order { get; set; }
    
    private Reservation()
    {
        
    }
    public Reservation(List<CartItem> reservedItem, Order order)
    {
        ReservedItem = reservedItem;
        CratedAt = DateTime.UtcNow;
        Status = ReservationStatus.Reserved;
        Order = order;
    }
}