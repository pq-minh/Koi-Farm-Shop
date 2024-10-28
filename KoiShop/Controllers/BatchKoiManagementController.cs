using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/batchkois/management")]
public class BatchKoiManagementController : ControllerBase
{
    private readonly IBatchKoiService _batchKoiService;

    public BatchKoiManagementController(IBatchKoiService batchKoiService)
    {
        _batchKoiService = batchKoiService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAllBatchKoi()
    {
        var allBatchKoi = await _batchKoiService.GetAllBatchKoiStaff();
        if(allBatchKoi == null)
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

