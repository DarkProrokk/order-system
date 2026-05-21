namespace Domain.Entity;

public class Item: Entity
{
    public decimal Price { get; set; }
    public string Name { get; set; }
    public Seller Seller { get; set; }
}