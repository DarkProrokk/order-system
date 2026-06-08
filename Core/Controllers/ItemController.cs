using Application.Interfaces.Services;
using Application.Model;
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

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ItemQueryDto dto)
    {
        return Ok(await service.GetAsync(dto.Map()));
    }
}