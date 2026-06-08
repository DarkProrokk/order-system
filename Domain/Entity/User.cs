namespace Domain.Entity;

public class User: Entity
{
    public string? Email { get; set; }
    public Cart Cart { get; set; } = new Cart();
}