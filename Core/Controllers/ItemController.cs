using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController(IItemService service): ControllerBase
{
    [HttpGet]
    [Route("test")]
    public IActionResult Test([FromQuery] int count)
    {
        service.GenerateTestData(count);
        return Ok();
    }
}