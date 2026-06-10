using Domain.Exception;

namespace Domain.Entity;

public class CartItem: Entity
{
    public Cart Cart { get; set; }
    public Item? Item { get; set; }
    
    public int ItemId { get; set; }
    public int Quantity { get; set; } = 1;

    private CartItem()
    {
    }

    private CartItem(Cart cart, Item item, int quantity)
    {
        Cart = cart;
        Item = item;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    private CartItem(Cart cart, int itemId, int quantity)
    {
        Cart = cart;
        ItemId = itemId;
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        Quantity = quantity;
    }
    
    public static CartItem Create(Cart cart, Item item, int quantity) => new(cart, item, quantity);
    public static CartItem Create(Cart cart, int itemId, int quantity) => new(cart, itemId, quantity);
}