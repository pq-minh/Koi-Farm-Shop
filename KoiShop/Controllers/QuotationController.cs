using KoiShop.Application.Command.CreateRequest;
using KoiShop.Application.Command.UpdatePriceQuotation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/quotation")]
    public class QuotationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("UpdatePrice")]
        public async Task<IActionResult> CreateRequest([FromBody] UpdatePriceQuotationCommand updatePriceQuotationCommand)
        {
            var result = await mediator.Send(updatePriceQuotationCommand);
            return Ok(result);
        }
    }
}
