using Domain.Entity;
using Domain.Exception;

namespace Tests.Domain.Entity;

public class ItemTest
{
    [Fact]
    public void CreateItem_WithValid_ShouldCreate()
    {
        //Arrange & Act
        var item = Item.Create(55, "Item1", 44);
        //Assert
        Assert.NotNull(item);
        Assert.Equal("Item1", item.Name);
        Assert.Equal(55, item.Price);
        Assert.Equal(44, item.Quantity);
    }
    
    
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CreateItem_WithInvalidPrice_ShouldException(decimal price)
    {
        //Arrange & Act
        //Assert
        Assert.Throws<DomainException>(() => Item.Create(price, "Item1", 44));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CreateItem_WithInvalidQuantity_ShouldException(int quantity)
    {
        //Arrange & Act
        //Assert
        Assert.Throws<DomainException>(() => Item.Create(55, "Item1", quantity));
    }

    [Fact]
    public void IncreasedStock_WithValid_ShouldIncreaseAndSuccess()
    {
        var item = Item.Create(55, "Item1", 44);
        var increasedResult = item.IncreaseStock(10);
        Assert.Equal(54, item.Quantity);
        Assert.True(increasedResult.IsSuccess);
    }
    
    
    [Theory]
    [InlineData(5)]
    [InlineData(30)]
    [InlineData(44)]
    public void ReduceStock_WithValid_ShouldReduceAndSuccess(int quantity)
    {
        var initialQuantity = 44;
        var item = Item.Create(55, "Item1", initialQuantity);
        var reduceResult = item.ReduceStock(quantity);
        Assert.Equal(initialQuantity - quantity, item.Quantity);
        Assert.True(reduceResult.IsSuccess);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void IncreasedStock_WithInvalidDelta_ShouldFailureAndNotReduce(int reduceQuantity)
    {
        var item = Item.Create(55, "Item1", 44);
        var increaseResult = item.IncreaseStock(reduceQuantity);
        Assert.True(increaseResult.IsFailure);
        Assert.Equal(44, item.Quantity);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(44)]
    public void CanReserve_WithValidQuantity_ShouldTrue(int quantity)
    {
        var item = Item.Create(55, "Item1", 44);
        var result = item.CanReserve(quantity);
        Assert.True(result);
    }
    
    [Theory]
    [InlineData(45)]
    [InlineData(46)]
    public void CanReserve_WithInvalidQuantity_ShouldFalse(int quantity)
    {
        var item = Item.Create(55, "Item1", 44);
        var result = item.CanReserve(quantity);
        Assert.False(result);
    }
}