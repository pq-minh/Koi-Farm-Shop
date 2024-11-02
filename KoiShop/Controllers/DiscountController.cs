using KoiShop.Application.Command.CreateDiscount;
using KoiShop.Application.Command.UpdateDiscount;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/discounts")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        private readonly IMediator _mediator;
        public DiscountController(IDiscountService discountService, IMediator mediator)
        {
            _discountService = discountService;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetDiscount()
        {
            var discounts = await _discountService.GetDiscount();
            return Ok(discounts);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetDiscountForUser()
        {
            var discounts = await _discountService.GetDiscountForUser();
            if (discounts != null)
                return Ok(discounts);
            else
                return BadRequest("Fail");
        }
        [HttpPost]
        public async Task<IActionResult> GetDiscount([FromBody] DiscountDtoV2 discount)
        {
            var name = discount.Name;
            var discounts = await _discountService.GetDiscountForUser(name);
            if (discount != null)
                return Ok(discounts);
            else
                return BadRequest("Fail");

        }

        [HttpPost("updatediscount")]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountCommand updateDiscountCommand)
        {
            var result = await _mediator.Send(updateDiscountCommand);
            return Ok(result); 
        }

        [HttpPost("creatediscount")]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand createDiscountCommand)
        {
            var result = await _mediator.Send(createDiscountCommand);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }
    }
}
