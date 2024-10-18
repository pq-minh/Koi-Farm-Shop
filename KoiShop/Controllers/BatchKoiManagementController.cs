using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/batchkois/management")]
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
        [HttpGet]
        public async Task<IActionResult> GetAllBatchKoi()
        {
            var all = await _batchKoiService.GetAllBatchKoiStaff();
            if (all == null) return NotFound();
            return Ok(all);
        }

        [HttpGet("{batchKoiId:int}")]
        public async Task<IActionResult> GetKoiById(int batchKoiId)
        {
            var koi = await _batchKoiService.GetBatchKoiById(batchKoiId);
            return Ok(koi);
        }

        [HttpPost]
        public async Task<IActionResult> AddBatchKoi([FromForm] AddBatchKoiDto batchKoiDto)
        {
            if (!await _batchKoiService.ValidateAddBatchKoiDtoInfo(batchKoiDto))
                return BadRequest("You have not entered Batch Koi information or the Batch Koi info is invalid.");

            var koiImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.ImageFile, "KoiFishImage");
            var cerImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.ImageFile, "KoiFishCertificate");

            if (koiImageUrl == null || cerImageUrl == null)
                return BadRequest("You have not entered Batch Koi information or the Batch Koi info is invalid.");

            var result = await _batchKoiService.AddBatchKoi(batchKoiDto, koiImageUrl, cerImageUrl);

            if (!result)
                return BadRequest("Failed to add Batch Koi.");

            return Ok("Batch Koi added successfully.");
        }


        [HttpPut("{batchKoiId:int}")]
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

        [HttpPut("{batchKoiId:int}-{status}")]
        public async Task<IActionResult> UpdateBatchKoiStatus(int batchKoiId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("You have not entered Koi information or the Koi info is invalid.");
            }

            var result = await _batchKoiService.UpdateBatchKoiStatus(batchKoiId, status);

            if (result)
            {
                return Ok("Koi status updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update Koi.");
            }
        }

        // KoiCategory Methods ======================================================================================
        [HttpGet("category")]
        public async Task<IActionResult> GetAllBatchKoiCategory()
        {
            var all = await _batchKoiService.GetAllBatchKoiCategory();
            return Ok(all);
        }
    }

}
