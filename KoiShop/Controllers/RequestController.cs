using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KoiShop.Application.Command.CreateRequest;
using MediatR;
namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/request")]
    public class RequestController(IMediator mediator) : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestCommand createRequestCommand)
        {
           var result = await mediator.Send(createRequestCommand);
            return Ok(result);           
        }

    }
}
