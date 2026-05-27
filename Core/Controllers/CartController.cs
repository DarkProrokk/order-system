using Application.Extensions;
using Application.Interfaces;
using Application.Model;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;
[ApiController]
[Route("cart/")]
public class CartController(ILogger<CartController> logger, ICartService cartService): ControllerBase
{
    [HttpPost]
    [Route("items")]
    public IActionResult AddItem(AddItemInCartModel model)
    {
        logger.LogInformation("Receiving request");
        var result = cartService.AddItem(model);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}