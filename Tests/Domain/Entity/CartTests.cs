using Domain.Entity;

namespace Tests.Domain.Entity;

public class CartTests
{
    private static User CreateUser()
    {
        var user = User.Create("123@mail.ru");
        user.Id = 1;
        return user;
    }

    [Fact]
    public void CrateCart_ValidWithUserEntity_ShouldCreate()
    {
        var user = CreateUser();
        var cart = Cart.Create(user);
        Assert.Equal(user, cart.User);
    }
    
    [Fact]
    public void CrateCart_ValidWithUserId_ShouldCreate()
    {
        
        var cart = Cart.Create(CreateUser().Id);
        Assert.Equal(CreateUser().Id, cart.UserId);
    }
}