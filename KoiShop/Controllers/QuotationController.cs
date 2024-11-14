using KoiShop.Application.Command.CreateRequest;
using KoiShop.Application.Command.DecisionRequest;
using KoiShop.Application.Command.UpdatePriceQuotation;
using KoiShop.Application.Queries.GetQuotation;
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
        [HttpPost("updateprice")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceQuotationCommand updatePriceQuotationCommand)
        {
            var result = await mediator.Send(updatePriceQuotationCommand);
            return Ok(result);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-quotation")]
        public async Task<IActionResult> GetQuotation(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetQuotationQuery(pageNumber, pageSize);
            var result = await mediator.Send(query);
            return Ok(result);
        }

    }
}
