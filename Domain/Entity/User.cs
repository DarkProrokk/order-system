namespace Domain.Entity;

public class User: Entity
{
    public string? Email { get; set; }
    public Cart? Cart { get; set; } 
    
    private User() {}
    
    private  User(string email)
    {
        Email = email;
    }

    public Result.Result AddCart(Cart cart)
    {
        if (Cart != null) return Result.Result.Failure("Cart already exists");
        Cart = cart;
        return Result.Result.Success();
    }

    public static User Create(string email) => new User(email);
}