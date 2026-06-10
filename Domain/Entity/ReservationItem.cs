using Domain.Exception;

namespace Domain.Entity;

public class ReservationItem
{
    public Item Item { get; set; }
    public int Quantity { get; set; } = 1;
    
    
    private ReservationItem(){}
    
    
    private ReservationItem(Item item,int quantity)
    {
        Item = item;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    
    public static ReservationItem Create(Item item, int quantity) => new(item,quantity);
}