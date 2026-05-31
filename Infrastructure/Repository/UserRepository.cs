using Application.Extensions;
using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class UserRepository(OrderContext context) : Repository<User>(context), IUserRepository
{
    public async Task AddTestData(int count)
    {
        using var activity = Trace.StartActivity("UserRepository.AddTestData");
        for (int i = 1; i <= count; i++)
        {
            var user = new User() {Email = $"testUser{i}@mail.com"};
            await AddAsync(user);
        }
        await SaveChangesAsync();
    }
}