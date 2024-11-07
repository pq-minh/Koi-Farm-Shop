using KoiShop.Application.Dtos.OrderDtos;
using KoiShop.Application.Dtos.Payments;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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


        //[HttpGet("get")]
        //public async Task<IActionResult> GetOrders([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        //{
        //    var orders = await _orderService.GetOrders(status, startDate, endDate);
        //    if (orders == null)
        //        return BadRequest("Order not found.");
        //    return Ok(orders);
        //}



        //[HttpGet("get-all")]
        //public async Task<IActionResult> GetAllOrders()
        //{
        //    var orders = await _orderService.GetAllOrders();
        //    if (orders == null)
        //        return BadRequest("Order not found.");
        //    return Ok(orders);
        //}


        //[HttpGet("get-by-status")]
        //public async Task<IActionResult> GetOrdersByStatus(string status)
        //{
        //    var orders = await _orderService.GetOrdersByStatus(status);
        //    if (orders == null)
        //        return BadRequest("Order not found.");
        //    return Ok(orders);
        //}

        //[HttpGet("get-details")]
        //public async Task<IActionResult> GetOrderDetails([FromQuery] string status, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        //{
        //    var orderDetails = await _orderService.GetOrderDetails(status, startDate, endDate);
        //    if (orderDetails == null)
        //        return BadRequest("OrderDetails not found.");
        //    return Ok(orderDetails);
        //}


        [HttpGet("best-sales/koi")]
        public async Task<IActionResult> GetBestSalesKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var id = await _orderService.GetBestSalesKoi(startDate, endDate);
            if (id == -1)
                return BadRequest("No koi fish for sale yet.");

            return Ok(id);
        }

        [HttpGet("best-sales/batch-koi")]
        public async Task<IActionResult> GetBestSalesBatchKoi([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var id = await _orderService.GetBestSalesBatchKoi(startDate, endDate);
            if (id == -1)
                return BadRequest("No batch koi for sale yet.");

            return Ok(id);
        }


        [HttpGet("total")]
        public async Task<IActionResult> GetTotalOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.CountTotalOrders(startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("complete")]
        public async Task<IActionResult> GetCompletedOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.CountOrders("Completed", startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.CountOrders("Pending", startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("shipped")]
        public async Task<IActionResult> GetShippedOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.CountOrders("Shipped", startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }

        [HttpGet("intransit")]
        public async Task<IActionResult> GetInTransitOrders([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var total = await _orderService.CountOrders("InTransit", startDate, endDate);
            if (total == 0)
                return BadRequest("No order exists.");

            return Ok(total);
        }


        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateOder(UpdateOrderDtos order)
        //{
        //    var result = await _orderService.UpdateOrder(order);
        //    if (result)
        //        return Ok("Order updated successfully.");

        //    return BadRequest("Order updated unsuccessfully.");
        //}

        [HttpPut("update/status")]
        public async Task<IActionResult> UpdateOderStatus(OrderDtoUpdateStatus orderDtoUpdateStatus)
        {
            var result = await _orderService.UpdateOrderStatus(orderDtoUpdateStatus.OrderID, orderDtoUpdateStatus.Status);
            if (result)
                return Ok("Order status updated successfully.");

            return BadRequest("Order status updated unsuccessfully.");
        }

        [HttpPut("update-payment/status")]
        public async Task<IActionResult> UpdatePaymentStatus([FromForm] int paymentId, [FromForm] string status)
        {
            var result = await _orderService.UpdatePaymentStatus(paymentId, status);
            if (result)
                return Ok("Payment status updated successfully.");

            return BadRequest("Payment status updated unsuccessfully.");
        }

        //[HttpGet("get-details")]
        //public async Task<IActionResult> GetOrderDetailsInOrder(int orderId)
        //{
        //    var orderDetails = await _orderService.GetOrderDetailsInOrder(orderId);

        //    if (orderDetails.IsNullOrEmpty())
        //        return BadRequest("No order detail found.");

        //    return Ok(orderDetails);
        //}

        [HttpGet("get-payments")]
        public async Task<ActionResult> GetAllPayments()
        {
            var payments = await _orderService.GetAllPayments();

            if (payments == null || payments.Count() == 0)
                return BadRequest("No payment found.");

            return Ok(payments);
        }


        [HttpGet("get-payments/status")]
        public async Task<ActionResult> GetPaymentsByStatus(string status)
        {
            var payments = await _orderService.GetPaymentsByStatus(status);

            if (payments == null || payments.Count() == 0)
                return BadRequest("No payment found.");

            return Ok(payments);
        }


        [HttpGet("get-payments/between")]
        public async Task<ActionResult> GetAllPayments(DateTime startDate, DateTime endDate)
        {
            var payments = await _orderService.GetPaymentsBetween(startDate, endDate);

            if (payments == null || payments.Count() == 0)
                return BadRequest("No payment found.");

            return Ok(payments);
        }

        [HttpGet("get-order-details")]
        public async Task<ActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderService.GetAllOrderDetails();

            if (orderDetails == null || orderDetails.Count() == 0)
                return BadRequest("No Order Details found.");

            return Ok(orderDetails);
        }

    }
}
