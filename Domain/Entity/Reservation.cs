using Domain.Enum;

namespace Domain.Entity;

public class Reservation: Entity
{
    public List<CartItem> ReservedItem { get; set; }
    public DateTime CratedAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    private Reservation()
    {
        
    }
    public Reservation(List<CartItem> reservedItem)
    {
        ReservedItem = reservedItem;
        CratedAt = DateTime.UtcNow;
        Status = ReservationStatus.Reserved;
    }
}