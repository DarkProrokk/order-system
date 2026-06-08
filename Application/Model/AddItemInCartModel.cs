namespace Application.Model;

public class AddItemInCartModel
{
    public int UserId { get; set; }
    public int ItemId { get; set; }

    public int Quantity { get; set; } = 1;
}