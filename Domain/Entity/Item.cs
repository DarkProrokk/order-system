using Domain.Exception;

namespace Domain.Entity;

public class Item: Entity
{
    public decimal Price { get; private set; }
    public string? Name { get; private set; }


    private Item()
    {
    }
    
    public Item(decimal price, string name)
    {
        if(price <= 0) throw new DomainException("invalid_price", "Price must be greater than zero");
        Price = price;
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("invalid_name", "Name must not be empty");
        Name = name;
    }
}