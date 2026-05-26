using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController(IItemService service): ControllerBase
{
    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        service.GenerateTestData(10);
        return Ok();
    }
}