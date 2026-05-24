namespace Domain.Entity;

public class CartItem: Entity
{
    public Cart Cart { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; } = 1;

    public CartItem()
    {

    }
}