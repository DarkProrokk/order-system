using Domain.Exception;

namespace Domain.Entity;

public class OrderItem
{
    public int ItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;


    private OrderItem()
    {
        
    }
    
    private OrderItem(int itemId, decimal price, int quantity)
    {
        ItemId = itemId;
        Price = price;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    
    public static OrderItem Create(Item item, int quantity) => new(item.Id, item.Price,quantity);
}