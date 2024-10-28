using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/kois/management")]
    public class KoiManagementController : ControllerBase
    {
        private readonly IKoiService _koiService;

        public KoiManagementController(IKoiService koiService)
        {
            _koiService = koiService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetKois()
        {
            var allKoi = await _koiService.GetAllKoiStaff();
            if (allKoi == null) 
                return NotFound();
            return Ok(allKoi);
        }

        [HttpGet("get/{koiId}")]
        public async Task<IActionResult> GetKoiById(int koiId)
        {
            var koi = await _koiService.GetKoiById(koiId);
            if(koi == null) 
                return NotFound();
            return Ok(koi);
        }

        [HttpPost]
        public async Task<IActionResult> AddKoi([FromForm] AddKoiDto koiDto)
        {
            if (koiDto == null)
                return BadRequest("Koi information is missing or invalid.");

            var result = await _koiService.AddKoi(koiDto);
            return result ? Ok("Koi added successfully.") : BadRequest("Failed to add Koi.");
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateKoi([FromForm] UpdateKoiDto koiDto)
        {
            var result = await _koiService.UpdateKoi(koiDto);
            return result ? Ok("Koi updated successfully.") : BadRequest("Failed to update Koi.");
        }

        [HttpPut("update/{koiId}-{status}")]
        public async Task<IActionResult> UpdateKoiStatus(int koiId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest("Invalid status.");

            var result = await _koiService.UpdateKoiStatus(koiId, status);
            return result ? Ok("Koi status updated successfully.") : BadRequest("Failed to update Koi status.");
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetAllKoiCategory()
        {
            var allCategories = await _koiService.GetAllKoiCategory();
            if(allCategories == null)
                return NotFound();
            return Ok(allCategories);
        }
    }
}
