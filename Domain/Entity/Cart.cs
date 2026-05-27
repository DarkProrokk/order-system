using System.ComponentModel.DataAnnotations.Schema;
using Domain.Exception;

namespace Domain.Entity;

public class Cart: Entity
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public User? User { get; set; }
    
    public int? UserId { get; set; }

    public Cart()
    {
    }

    public void Add(Item item)
    {
        if (UserId == null) throw new DomainException("cart_with_null_user", "Cannot add an item in cart without user");
        var entity = Items.Find(e => e.Item.Id == item.Id);
        if (entity == null)
        {
            var cartItem = new CartItem();
            cartItem.Cart = this;
            cartItem.Item = item;
            Items.Add(cartItem);
        }
        else
        {
            ChangeItemQuantity(entity.Id, entity.Quantity+1);
        }
    }

    public void ChangeItemQuantity(int itemId, int quantity)
    {
        if (quantity <= 0) throw new DomainException("invalid_quantity", "Quantity must be greater than zero");
        var item = GetCartItemById(itemId);
        item.Quantity = quantity;
    }

    public void Remove(int itemId)
    {
        var item = GetCartItemById(itemId);
        Items.Remove(item);
    }
    /// <summary>
    /// Find <see cref="CartItem"/> by <see cref="Item"/> id
    /// </summary>
    /// <param name="itemId">id an <see cref="Item"/></param>
    /// <returns><see cref="CartItem"/></returns>
    /// <exception cref="DomainException">Throw if item not contains in cart</exception>
    public CartItem GetCartItemById(int itemId)
    {
        var item = Items.Find(item => item.Id == itemId);
        if (item == null) throw new DomainException("not_found_cart_item", $"Item with id {itemId} not found in " +
                                                                           $"cart with id : {Id}");
        return item;
    }
}