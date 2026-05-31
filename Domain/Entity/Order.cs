using Domain.Enum;

namespace Domain.Entity;

public class Order: Entity
{
    public List<Item>? Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Reservation Reservation { get; set; }
}