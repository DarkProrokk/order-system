using Domain.Entity;

namespace Tests.Domain.Entity;

public class CartItemTests
{

    private static Cart CreateCart()
    {
        var cart = Cart.Create(5);
        cart.Id = 5;
        return cart;
    }

    private static Item CreateItem() => Item.Create(55, "Item1", 5);
    [Fact]
    public void CreateCartItem_WithValidData_ShouldCreated()
    {
        var cartItem = CartItem.Create(CreateCart(), CreateItem(), 3);
        Assert.Equal(cartItem.Item);
    }
        
}