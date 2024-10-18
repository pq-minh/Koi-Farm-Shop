using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/discounts")]
    public class DiscountController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public DiscountController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDiscount()
        {
            var discounts = _orderService.GetDiscount();
            return Ok();
        }
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetDiscountForUser()
        {
            var discounts = _orderService.GetDiscountForUser();
            if (discounts != null)
                return Ok();
            else
                return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> GetDiscount([FromBody] DiscountDtoV2 discount)
        {
            var name = discount.Name;
            var discounts = await _orderService.GetDiscountForUser(name);
            if (discount != null)
                return Ok();
            else
                return BadRequest();

        }
    }
}
