using Domain.Exception;

namespace Domain.Entity;

public class Item: Entity
{
    public decimal Price { get; private set; }
    public string? Name { get; private set; }
    
    public int Quantity { get; private set; }


    private Item()
    {
    }
    
    public Item(decimal price, string name, int quantity = 1)
    {
        if(price <= 0) throw new DomainException("invalid_price", "Price must be greater than zero");
        Price = price;
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("invalid_name", "Name must not be empty");
        Name = name;
        if(quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }

    public void AdjustQuantity(int quantity)
    {
        if(Quantity + quantity < 0) throw new DomainException("insufficient_quantity", "Resulting quantity cannot be negative");
        Quantity += quantity;
    }
}