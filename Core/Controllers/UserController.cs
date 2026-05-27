using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService): ControllerBase
{
    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        userService.GenerateTestData();
        return Ok();
    }
}