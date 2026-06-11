using Domain.Exception;

namespace Domain.Entity;

public class Item: Entity
{
    public decimal Price { get; set; }
    public string? Name { get; set; }
    public int Quantity { get;  set; }

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
    
    public static Item Create(decimal price, string name, int quantity = 1) => new Item(price, name, quantity);

    public Result.Result ReduceStock(int quantity)
    {
        if(quantity <= 0) 
            return Result.Result.Failure("Quantity must be greater than zero");
        if (Quantity - quantity < 0) return Result.Result.Failure("Item out of stock");
        Quantity -= quantity;
        return Result.Result.Success();
    }
    
    public Result.Result IncreaseStock(int quantity)
    {
        if(quantity <= 0) 
            return Result.Result.Failure("Quantity must be greater than zero");
        Quantity += quantity;
        return Result.Result.Success();
    }

    public bool CanReserve(int quantity) => Quantity >= quantity;
}