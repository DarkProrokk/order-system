// using Application.Interfaces;
// using Application.Interfaces.Repository;
// using Application.Interfaces.Services;
// using Application.Services;
// using AutoFixture;
// using AutoFixture.AutoMoq;
// using Domain.Entity;
// using Microsoft.Extensions.Logging;
// using Moq;
//
// namespace Tests.Application.Service;
//
// public class OrderServiceTests
// {
//     [Fact]
//     public void OrderShouldBeCreated()
//     {
//         var fixture = new Fixture().Customize(new AutoMoqCustomization());
//         var service = fixture.Create<OrderService>();
//         
//         var userRepo = fixture.Freeze<Mock<IUserRepository>>();
//         var cartRepo = fixture.Freeze<Mock<ICartRepository>>();
//         
//         var item1 = new Item(55, "Item1", 20);
//         var user = new User();
//         user.Id = 1;
//         user.Email = "testUser1";
//         var cart1 = new Cart();
//         user.Carts?.Add(cart1);
//         var cartItem = new CartItem();
//         cartItem.Cart = cart1;
//         cartItem.Item = item1;
//         cartItem.Quantity = 10;
//         
//         userRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);
//         
//         cartRepo.Setup(x => x.GetByUserIdAsync(1)).ReturnsAsync(cart1);
//         
//         
//         //cartRepo.Setup(c => c.AddItemInCartAsync())
//         //repo.Setup(x => x.CreateOrder());
//     }
// }