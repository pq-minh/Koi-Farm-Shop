using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KoiShop.Application.Command.CreateRequest;
using MediatR;
using KoiShop.Application.Queries.GetAllRequest;
using KoiShop.Application.Command.DecisionRequest;
namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/request")]
    public class RequestController(IMediator mediator) : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-request")]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestCommand createRequestCommand)
        {
           var result = await mediator.Send(createRequestCommand);
            return Ok(result);           
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-request")]
        public async Task<IActionResult> GetRequest()
        {
            var result = await mediator.Send(new GetAllRequestQuery() );
            return Ok(result);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("decision-request")]
        public async Task<IActionResult> RequestDecision([FromBody] DecisionRequestCommand decisionRequestCommand)
        {
            var result = await mediator.Send(decisionRequestCommand);
            return Ok(result);
        }
    }
}
