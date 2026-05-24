using Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class OrderContext: DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {}
    
 }