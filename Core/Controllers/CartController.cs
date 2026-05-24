using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;
[ApiController]
[Route("cart")]
public class CartController(ILogger<CartController> logger): ControllerBase
{
    // [HttpPost]
    // [Route("/addItem")]
    // public IActionResult AddItem()
    // {
    //     logger.LogInformation();
    // }
}