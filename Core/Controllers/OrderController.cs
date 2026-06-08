using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService, ILogger<OrderController> logger): ControllerBase
{
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]int userId)
    {
        var result = await orderService.CreateOrder(userId);
        if (result.IsSuccess)
        {
            logger.LogInformation("Order created successfully {result}", result);
            return Ok(result);
        }
        logger.LogWarning("Order creation failed {result}", result);
        return BadRequest(result);
    }
}