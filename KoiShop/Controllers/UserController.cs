
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using KoiShop.Domain.Entities;
using MediatR;
using KoiShop.Application.Users.Queries.Login;
using KoiShop.Application.Users.Command.RegisterUser;
namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
       
       [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var token = await mediator.Send(new LoginUserQuery(model));
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRQ registerRequest)
        {
            var result = await mediator.Send(new RegisterUserCommand(registerRequest));
            if (!result)
            {
                return BadRequest(result);
            }
            return Created();
        }
    
    }
}
