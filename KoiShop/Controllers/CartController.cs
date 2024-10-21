using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cart = await _cartService.GetCart();
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest("Unexpected error"); 
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDtoV1 cart)
        {
            var result = await _cartService.AddCarts(cart);
            switch (result)
            {
                case CartEnum.Success:
                    return Ok("Add Succesfully.");
                case CartEnum.Fail:
                    return BadRequest("Have bug in add to cart.");
                case CartEnum.NotLoggedInYet:
                    return Unauthorized("User is not login");
                case CartEnum.UserNotAuthenticated:
                    return Unauthorized("User is not authenticated.");
                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCart([FromBody] CartDtoV1 cart)
        {
            var result = await _cartService.RemoveCart(cart);
            switch (result)
            {
                case CartEnum.Success:
                    return Ok("Remove Succesfully.");
                case CartEnum.Fail:
                    return BadRequest("Have bug in remove cart.");
                case CartEnum.NotLoggedInYet:
                    return Unauthorized("User is not login");
                case CartEnum.UserNotAuthenticated:
                    return Unauthorized("User is not authenticated.");
                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateBatchQuantity([FromBody] CartDtoV3 cart)
        {
            var status = cart.Status;
            var batchKoiId = cart.BatchKoiId;
            var result = await _cartService.ChangeBatchQuantity(status, batchKoiId);
            if (result)
                return Ok("Update successfull");
            else
                return BadRequest("An expected error occurred");
        }
    }
}
