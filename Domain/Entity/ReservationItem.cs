using Domain.Exception;

namespace Domain.Entity;

public class ReservationItem
{
    public Item Item { get; set; }
    public int Quantity { get; set; } = 1;
    
    public int ItemId { get; set; }
    
    
    private ReservationItem(){}
    
    
    private ReservationItem(Item item,int quantity)
    {
        Item = item;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    
    private ReservationItem(int itemId,int quantity)
    {
        ItemId = itemId;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    
    public static ReservationItem Create(Item item, int quantity) => new(item,quantity);
    public static ReservationItem Create(int itemId, int quantity) => new(itemId,quantity);
}