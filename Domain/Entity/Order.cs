namespace Domain.Entity;

public class Order: Entity
{
    public List<Item> Items { get; set; }
    
}