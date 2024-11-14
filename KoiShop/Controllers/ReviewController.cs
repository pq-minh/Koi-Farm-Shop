﻿using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
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
        [HttpPost("comment")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDtoComment reviewDtos)
        {
            var result = await _reviewService.AddReview(reviewDtos);
            if (result)
            {
                return Created();
            }
            return BadRequest("Can not add review");
        }
        [HttpPost("star")]
        public async Task<IActionResult> AddReviewStar([FromBody] ReviewDtoStar reviewDtos)
        {
            var result = await _reviewService.AddReviewStars(reviewDtos);
            if (result)
            {
                return Created();
            }
            return BadRequest("Can not add star");
        }
        [HttpPost]
        public async Task<IActionResult> AddAllReview([FromBody] ReviewAllDto reviewDtos)
        {
            var result = await _reviewService.AddAllReview(reviewDtos);
            if (result)
            {
                return Created();
            }
            return BadRequest("Can not add review");
        }
        [HttpGet("staff")]
        public async Task<IActionResult> GetReviewByStaff()
        {
            var review = await _reviewService.GetReviewByStaff();
            if (review != null)
            {
                return Ok(review);
            }
            return BadRequest("Not found any reviews");
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
        [HttpGet("/api/user/koi")]
        public async Task<IActionResult> GetKoiFromOrderDetail()
        {
            var koi = await _reviewService.GetKoiFromOrderDetail();
            return Ok(koi);
        }
        [HttpGet("/api/user/batch")]
        public async Task<IActionResult> GetBatchKoiFromOrderDetail()
        {
            var batch = await _reviewService.GetBatchFromOrderDetail();
            return Ok(batch);
        }
        [HttpGet("orderdetail")]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            var orderDetail = await _reviewService.GetAllOrderDetail();
            return Ok(orderDetail);
        }
    }
}
