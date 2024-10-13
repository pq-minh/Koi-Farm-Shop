using KoiFarmShop.Application.Interfaces;
using KoiFarmShop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;

        public DashboardController (IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("completed-orders")]
        public async Task<IActionResult> GetCompletedOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var completedOrders = await _orderService.GetCompletedOrdersAsync(startDate, endDate);
            return Ok(completedOrders);
        }
    }
}
