using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService): ControllerBase
{
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]int userId)
    {
        var result = await orderService.CreateOrder(userId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}