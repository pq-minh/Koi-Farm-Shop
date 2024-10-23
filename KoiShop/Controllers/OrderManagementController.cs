using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/orders/management")]
    public class OrderManagementController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderManagementController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var orders = await _orderService.GetOrders(status, startDate, endDate);
            return Ok(orders);
        }

        [HttpGet("order-details")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var orderDetails = await _orderService.GetOrderDetails(status, startDate, endDate);
            return Ok(orderDetails);
        }


        [HttpGet("koi/best-sales")]
        public async Task<IActionResult> GetBestSalesKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var id = await _orderService.GetBestSalesKoi(startDate, endDate);
            if(id == -1)
                return BadRequest("No koi fish for sale yet.");

            return Ok(id);
        }

        [HttpGet("batch-koi/best-sales")]
        public async Task<IActionResult> GetBestSalesBatchKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var id = await _orderService.GetBestSalesBatchKoi(startDate, endDate);
            if (id == -1)
                return BadRequest("No batch koi for sale yet.");

            return Ok(id);
        }


        [HttpGet("orders/total")]
        public async Task<IActionResult> GetTotalOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.GetTotalOrders(startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("orders/completed")]
        public async Task<IActionResult> GetCompletedOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.GetCompletedOrders(startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("orders/pending")]
        public async Task<IActionResult> GetPendingOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.GetPendingOrders(startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

    }
}
