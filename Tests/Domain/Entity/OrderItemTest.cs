using Domain.Entity;

namespace Tests.Domain.Entity;

public class OrderItemTest
{
    // private static OrderItem CreateOrderItem()
    // {
    //     
    // }

    private static Item CreateItem() => Item.Create(55, "Item1", 55);
    private static Cart CreateCart() => Cart.Create(5);
    
    
    [Fact]
    public void CreateCart_Wit()
    {}
}