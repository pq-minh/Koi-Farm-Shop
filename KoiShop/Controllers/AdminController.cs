using KoiShop.Application.Command.UpdatePointUser;
using KoiShop.Application.Users.Command.UpdateRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("/api/admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {

        [HttpPost("updaterole")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            var result = await mediator.Send(updateRoleCommand);
            return Ok(result);
        }

    }
}
