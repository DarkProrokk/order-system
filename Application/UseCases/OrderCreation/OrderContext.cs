using Domain.Entity;

namespace Application.UseCases.OrderCreation;

public record OrderContext(User user, Cart cart);