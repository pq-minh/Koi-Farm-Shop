
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using KoiShop.Domain.Entities;
using MediatR;
using KoiShop.Application.Users.Queries.Login;
using KoiShop.Application.Users.Command.RegisterUser;
using KoiShop.Application.Users.Command.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KoiShop.Application.Users.Command.ChangePassword;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateDetails(UpdateUserCommands command)
        {
            var token = await mediator.Send(command);
            return Ok(new { Token = token });
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand)
        {
            var result = await mediator.Send(changePasswordCommand);
            return Ok(new { Result = result });
        }
    }
}
