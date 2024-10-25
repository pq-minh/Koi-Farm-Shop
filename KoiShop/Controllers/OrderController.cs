using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.VnPayDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KoiShop.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;
        public OrderController(IOrderService orderService, IVnPayService vnPayService)
        {
            _orderService = orderService;
            _vnPayService = vnPayService;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetOrderDetail()
        //{
        //    var result = await _orderService.GetOrderDetail();
        //    return Ok(result);
        //}
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var result = await _orderService.GetOrder();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrderDetailById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderDtoV1 orderDto)
        {
            var carts = orderDto.Carts;
            var method = orderDto.Method;
            var discountId = orderDto.DiscountId;
            var phoneNumber = orderDto.PhoneNumber;
            var address = orderDto.Address;
            var request = orderDto.request;
            var result = await _orderService.AddOrders(carts, method, discountId, phoneNumber, address, request);
            switch (result)
            {
                case OrderEnum.Success:
                    return Created();
                case OrderEnum.Fail:
                    return BadRequest("Have bug in progrecing");
                case OrderEnum.FailAdd:
                    return BadRequest("Add have bug!!");
                case OrderEnum.FailUpdateCart:
                    return BadRequest("Update cart have bug");
                case OrderEnum.FailUpdateFish:
                    return BadRequest("Fail update fish");
                case OrderEnum.FailAddPayment:
                    return BadRequest("Add payment have bug");
                case OrderEnum.FailPaid:
                    var response = _vnPayService.ExecutePayment(request);
                    if (response == null || !response.Success)
                    {
                        var paymentFailMessage = $"PaymentFail {response?.VnPayResponseCode}";
                        var paymentResponse = new PaymentResponse
                        {
                            Status = OrderEnum.FailPaid,
                            Message = paymentFailMessage
                        };
                        return BadRequest(paymentResponse); // Trả về đối tượng PaymentResponse
                    }
                    else
                        return BadRequest("Unexpected error in payment");
                case OrderEnum.NotLoggedInYet:
                    return Unauthorized("User is not login");
                case OrderEnum.InvalidParameters:
                    return BadRequest("Miss a important parameter");
                case OrderEnum.InvalidTypeParameters:
                    return BadRequest("PhoneNumBer or address is not authenticated");
                case OrderEnum.UserNotAuthenticated:
                    return Unauthorized("User is not authenticated.");
                default:
                    return BadRequest("Unexpected error!");
            }
        }
        [HttpPost("createpayment")]
        public async Task<IActionResult> CreatePayment([FromBody] VnPaymentRequestModel paymentRequest)
        {
            var vnPayModel = _vnPayService.CreateVnpayModel(paymentRequest);
            if (vnPayModel == null)
            {
                return BadRequest("Invalid payment request");
            }
            var paymentUrl = await _vnPayService.CreatePatmentUrl(HttpContext, vnPayModel);
            return Ok(new { PaymentUrl = paymentUrl });
        }

    }
}
