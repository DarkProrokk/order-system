using Domain.Enum;

namespace Domain.Entity;

public class Reservation: Entity
{
    public List<CartItem> ReservedItems { get; set; }
    public DateTime CratedAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    private Reservation()
    {
        
    }
    private Reservation(Order order)
    {
        ReservedItems = order.Items;
        CratedAt = DateTime.UtcNow;
        Status = ReservationStatus.Reserved;
        Order = order;
    }

    public void Cancel()
    {
        Status = ReservationStatus.Canceled;
    }

    public static Reservation CreateFrom(Order order)
    {
        return new Reservation(order);
    }
}