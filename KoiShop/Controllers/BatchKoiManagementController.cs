using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Application.Users;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/batchkois/management")]
    public class BatchKoiManagementController : ControllerBase
    {
        private readonly IBatchKoiStaffService _batchKoiService;
        //private readonly IUserContext _userContext;
        //private readonly IUserStore<User> _userStore;

        public BatchKoiManagementController(IBatchKoiStaffService batchKoiStaffService/*, IUserContext userContext, IUserStore<User> userStore*/)
        {
            //_userContext = userContext;
            //_userStore = userStore;
            _batchKoiService = batchKoiStaffService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllBatchKoi()
        {
            //if (_userContext.GetCurrentUser() == null || _userStore == null)
            //    throw new ArgumentException("User context or user store is not valid.");
            //var userId = _userContext.GetCurrentUser().Id;
            //if (userId == null)
            //    return BadRequest();

            var allBatchKoi = await _batchKoiService.GetAllBatchKoiStaff();
            if (allBatchKoi == null)
                return NotFound("No Batch Koi found."); ;
            return Ok(allBatchKoi);
        }

        [HttpGet("get/{batchKoiId}")]
        public async Task<IActionResult> GetKoiById(int batchKoiId)
        {
            var koi = await _batchKoiService.GetBatchKoiById(batchKoiId);
            if (koi == null)
                return NotFound();
            return Ok(koi);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBatchKoi([FromForm] AddBatchKoiDto batchKoiDto)
        {
            if (batchKoiDto == null)
                return BadRequest("Invalid Batch Koi information.");

            var result = await _batchKoiService.AddBatchKoi(batchKoiDto);

            return result ? Ok("Batch Koi added successfully.") : BadRequest("Failed to add Batch Koi.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBatchKoi([FromForm] UpdateBatchKoiDto batchKoiDto)
        {
            if (batchKoiDto == null)
                return BadRequest("Invalid Batch Koi information.");

            var result = await _batchKoiService.UpdateBatchKoi(batchKoiDto);

            return result ? Ok("Batch Koi updated successfully.") : BadRequest("Failed to update Batch Koi.");
        }

        [HttpPut("update/{batchKoiId}-{status}")]
        public async Task<IActionResult> UpdateBatchKoiStatus(int batchKoiId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest("Status cannot be empty or exceed 50 characters.");

            var result = await _batchKoiService.UpdateBatchKoiStatus(batchKoiId, status);
            return result ? Ok("Batch Koi status updated successfully.") : BadRequest("Failed to update Batch Koi status.");
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetAllBatchKoiCategory()
        {
            var categories = await _batchKoiService.GetAllBatchKoiCategory();
            if (categories == null)
                return NotFound();
            return Ok(categories);
        }
    }
}
