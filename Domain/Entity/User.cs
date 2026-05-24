namespace Domain.Entity;

public class User: Entity
{
    public string? Email { get; set; }
    public List<Cart>? Carts { get; set; }
}