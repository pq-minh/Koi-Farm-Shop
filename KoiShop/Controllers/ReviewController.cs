using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        // afs
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> GetReview()
        {
            var review = await _reviewService.GetReview();
            if (review != null)
            {
                return Ok(review);
            }
            return BadRequest("Not found any reviews");
        }
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody]ReviewDtos reviewDtos)
        {
            var result = await _reviewService.AddReview(reviewDtos);
            if(result)
            {
                return Created();
            }
            return BadRequest("Can not add review");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromBody] ReviewDtoV1 reviewDtoV1)
        {
            var id = reviewDtoV1.Id;
            var result = await _reviewService.DeleteReview(id);
            if (result)
            {
                return NoContent();
            }
            return BadRequest("Can not remove review");
        }
        [HttpGet("api/user/koi")]
        public async Task<IActionResult> GetKoiFromOrderDetail()
        {
            var koi = await _reviewService.GetKoiFromOrderDetail();
            return Ok(koi);
        }
        [HttpGet("api/user/batch")]
        public async Task<IActionResult> GetBatchKoiFromOrderDetail()
        {
            var batch = await _reviewService.GetBatchFromOrderDetail();
            return Ok(batch);
        }
    }
}
