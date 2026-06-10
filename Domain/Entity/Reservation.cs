using Domain.Enum;
using Domain.Result;

namespace Domain.Entity;

public class Reservation: Entity
{
    public List<ReservationItem> ReservedItems { get; set; }
    public DateTime CratedAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    private Reservation()
    {
        
    }

    public Result<bool> ChangeStatus(ReservationStatus status)
    {
        if (Status == ReservationStatus.Completed) return Result<bool>.Failure("Cannot change " +
                                                                               "status for completed reservation");
        
        
        if (status == ReservationStatus.Canceled) return Result<bool>.Failure("Cannot change " +
                                                                              "status for canceled reservation");
        Status = status;
        return Result<bool>.Success();
    }
    
    private Reservation(Order order)
    {
        ReservedItems = order.Items;
        CratedAt = DateTime.UtcNow;
        Status = ReservationStatus.Reserved;
        Order = order;
    }

    public Result<bool> Cancel()
    {
        var changeStatusResult = ChangeStatus(ReservationStatus.Canceled);
        if  (changeStatusResult.IsFailure) return changeStatusResult;
        foreach (var reservationItem in ReservedItems)
        {
            reservationItem.Item.IncreaseStock(reservationItem.Quantity);
        }
        return Result<bool>.Success();
    }

    public static Reservation CreateFrom(Order order)
    {
        return new Reservation(order);
    }
}