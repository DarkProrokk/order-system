namespace Domain.Entity;

public class Order: Entity
{
    public List<CartItem>? Items { get; set; }
    
}