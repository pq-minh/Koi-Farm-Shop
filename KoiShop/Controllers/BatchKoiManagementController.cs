using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/batchkoi-management")]
    public class BatchKoiManagementController : ControllerBase
    {
        private readonly IBatchKoiService _batchKoiService;
        private readonly FirebaseService _firebaseService;

        public BatchKoiManagementController(IBatchKoiService batchKoiService, FirebaseService firebaseService)
        {
            _batchKoiService = batchKoiService;
            _firebaseService = firebaseService;
        }

        // Koi Methods =============================================================================================
        [HttpGet("get-batchkoi")]
        public async Task<IActionResult> GetAllBatchKoi()
        {
            var all = await _batchKoiService.GetAllBatchKoi();
            if (all == null) return NotFound();
            return Ok(all);
        }

        [HttpGet("get-batchkoi/{batchKoiId}")]
        public async Task<IActionResult> GetKoiById(int batchKoiId)
        {
            var koi = await _batchKoiService.GetBatchKoiById(batchKoiId);
            return Ok(koi);
        }

        [HttpPost("add-batchkoi")]
        public async Task<IActionResult> AddBatchKoi([FromForm] AddBatchKoiDto batchKoiDto)
        {
            if (!await _batchKoiService.ValidateAddBatchKoiDtoInfo(batchKoiDto))
            {
                return BadRequest("You have not entered Batch Koi information or the Batch Koi info is invalid.");
            }

            // upload ảnh lên firebase và trả về url ảnh
            var imageUrl = await _firebaseService.UploadFileToFirebaseStorageAsync(batchKoiDto.ImageFile, "KoiFishImage");

            if(imageUrl == null)
            {
                return BadRequest("You have not entered Batch Koi information or the Batch Koi info is invalid.");
            }
            var result = await _batchKoiService.AddBatchKoi(batchKoiDto, imageUrl);
            if (!result)
            {
                return BadRequest("Failed to add Batch Koi.");
            }
            return Ok("Batch Koi added successfully.");
        }


        [HttpPut("update-batchkoi/{batchKoiId}")]
        public async Task<IActionResult> UpdateBatchKoi(int batchKoiId, [FromForm] UpdateBatchKoiDto batchKoiDto)
        {
            BatchKoi batchKoi = await _batchKoiService.ValidateUpdateBatchKoiDto(batchKoiId, batchKoiDto);

            if(batchKoi != null)
            {
                var result = await _batchKoiService.UpdateBatchKoi(batchKoi);
                if (!result)
                    return BadRequest("Failed to update Batch Koi.");
            }
            else
            {
                return BadRequest("You have not entered Batch Koi information or the Batch Koi info is invalid.");
            }
            
            return Ok("Update Batch Koi successfully..");
        }

        // KoiCategory Methods ======================================================================================
        [HttpGet("get-batchkoi-category")]
        public async Task<IActionResult> GetAllBatchKoiCategory()
        {
            var all = await _batchKoiService.GetAllBatchKoiCategory();
            return Ok(all);
        }
    }

}
