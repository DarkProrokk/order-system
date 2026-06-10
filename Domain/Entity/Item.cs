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

    public Result.Result<Item> AdjustQuantity(int quantity)
    {
        if (Quantity - quantity < 0) return Result.Result<Item>.Failure(this, "Item out of stock");
        Quantity -= quantity;
        return Result.Result<Item>.Success();
    }

    public Result.Result<bool> ReduceStock(int quantity)
    {
        if(quantity < 0) 
            throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        if (Quantity - quantity < 0) return Result.Result<bool>.Failure("Item out of stock");
        Quantity -= quantity;
        return Result.Result<bool>.Success();
    }
    
    public Result.Result<bool> IncreaseStock(int quantity)
    {
        if(quantity < 0) 
            throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity += quantity;
        return Result.Result<bool>.Success();
    }

    public bool CanReserve(int quntity)
    {
        var result = Quantity - quntity;
        return result < 0;
    }
}