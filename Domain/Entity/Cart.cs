using System.ComponentModel.DataAnnotations.Schema;
using Domain.Exception;

namespace Domain.Entity;

public class Cart: Entity
{
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public User? User { get; set; }
    
    public int? UserId { get; set; }

    private Cart(){}
    
    public Cart(User user)
    {
        User = user;
    }

    public Cart(int userId)
    {
        UserId = userId;
    }

    public static Cart Create(int userId) => new Cart(userId);
    public static Cart Create(User user) => new Cart(user);

    public void Add(Item item, int quantity = 1)
    {
        var entity = CartItems.Find(e => e.Item.Id == item.Id);
        if (entity != null) ChangeItemQuantity(entity.Id, entity.Quantity+quantity);
        else
        {
            var cartItem = CartItem.Create(this,item, quantity);
            CartItems.Add(cartItem);
            
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
        CartItems.Remove(item);
    }
    /// <summary>
    /// Find <see cref="CartItem"/> by <see cref="Item"/> id
    /// </summary>
    /// <param name="itemId">id an <see cref="Item"/></param>
    /// <returns><see cref="CartItem"/></returns>
    /// <exception cref="DomainException">Throw if item not contains in cart</exception>
    public CartItem GetCartItemById(int itemId)
    {
        var item = CartItems.Find(item => item.ItemId == itemId);
        if (item == null) throw new DomainException("not_found_cart_item", $"Item with id {itemId} not found in " +
                                                                           $"cart with id : {Id}");
        return item;
    }

    public Result.Result<bool> ValidateForOrder()
    {
        if(CartItems.Count == 0) return Result.Result<bool>.Failure("Cart is empty");
        return  Result.Result<bool>.Success();
    }
}