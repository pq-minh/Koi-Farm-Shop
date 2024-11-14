﻿using KoiShop.Application.Command.UpdateAddress;
using KoiShop.Application.Users.Command.UpdateAddress;
using KoiShop.Application.Users.Queries.GetAllAddress;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/address")]
    public class AddressController(IMediator mediator) : Controller
    {

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-address")]
        public async Task<IActionResult> CreateAddress(CreateUserAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(new { Result = result });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getall-address")]
        public async Task<IActionResult> GetAllAddress()
        {
            var result = await mediator.Send(new GetAllAddressQuery());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("updateaddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand updateAddressCommand)
        {
            var result = await mediator.Send(updateAddressCommand);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
