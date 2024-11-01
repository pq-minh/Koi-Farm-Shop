﻿
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
using KoiShop.Application.Users.Command.Logout;
using KoiShop.Application.Users.Command.UpdateAddress;
using KoiShop.Application.Users.Command.ResetPassword;
namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMediator mediator ) : ControllerBase
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
            switch (result)
            {
                case "ChangePasswordConfirm":
                    return Ok(new { Result = result });
                case "Old Password incorrect":
                    return BadRequest(new { Message = "Old password is incorrect." });
                default:
                    return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await mediator.Send( new LogoutCommand());
            return Ok(new { Result = result });
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand resetPasswordCommand)
        {
            var result = await mediator.Send(resetPasswordCommand);
            return Ok(result);
        }
        [HttpPost("confirmpassword")]
        public async Task<IActionResult> ConfirmPasswordRest([FromBody] ConfirmPasswordCommand confirmPasswordCommand)
        {
            var result = await mediator.Send(confirmPasswordCommand);

            if (result.IsSuccess)
            {
                return Ok(new { Message = result.Message });
            }

            return BadRequest(new { Errors = result.Errors });
        }

    }
}
