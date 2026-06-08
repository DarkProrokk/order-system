using Application.Interfaces.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class ReservationController(IReservationService reservationService):ControllerBase
// {
//     [HttpGet]
//     [Route("expired")]
//     public async Task<IActionResult> GetExpired()
//     {
//         return Ok(await reservationService.GetExpired());
//     }
// }