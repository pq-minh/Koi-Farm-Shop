using KoiShop.Application.Dtos;
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
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    //var kois = await mediator.Send(new GetAllKoiQuery());
        //    //return Ok(kois);
        //}
        private readonly IKoiService _koiService;
        private readonly FirebaseService _firebaseService;

        public KoiManagementController(IKoiService koiService, FirebaseService firebaseService)
        {
            _koiService = koiService;
            _firebaseService = firebaseService;
        }

        // Koi Methods =============================================================================================
        [HttpGet]
        public async Task<IActionResult> GetAllKoiStaff()
        {
            var allKoi = await _koiService.GetAllKoi();
            if (allKoi == null) return NotFound();
            return Ok(allKoi);
        }

        [HttpGet("{koiId:int}")]
        public async Task<IActionResult> GetKoiById(int koiId)
        {
            var koi = await _koiService.GetKoiById(koiId);
            return Ok(koi);
        }


        [HttpPost]
        public async Task<IActionResult> AddKoi([FromForm] AddKoiDto koiDto)
        {
            if (!await _koiService.ValidateAddKoiDtoInfo(koiDto))
                return BadRequest("You have not entered Koi information or the Koi info is invalid.");

            var koiImageUrl = await _firebaseService.UploadFileToFirebaseStorage(koiDto.ImageFile, "KoiFishImage");
            var cerImageUrl = await _firebaseService.UploadFileToFirebaseStorage(koiDto.ImageFile, "KoiFishCertificate");

            if (koiImageUrl == null || cerImageUrl == null)
                return BadRequest("You have not entered Koi information or the Koi info is invalid.");

            var result = await _koiService.AddKoi(koiDto, koiImageUrl, cerImageUrl);
            if (!result)
                return BadRequest("Failed to add Koi.");

            return Ok("Koi added successfully.");
        }


        [HttpPut("{koiId:int}")]
        public async Task<IActionResult> UpdateKoi(int koiId, [FromForm] UpdateKoiDto koiDto)
        {
            Koi koi = await _koiService.ValidateUpdateKoiDto(koiId, koiDto);

            if (koi != null)
            {
                var result = await _koiService.UpdateKoi(koi);
                if (!result)
                    return BadRequest("Failed to update Koi.");
            }
            else
            {
                return BadRequest("You have not entered Koi information or the Koi info is invalid.");
            }

            return Ok("Batch Koi successfully..");
        }

        [HttpPut("{koiId:int}-{status}")]
        public async Task<IActionResult> UpdateKoiStatus(int koiId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("You have not entered Koi information or the Koi info is invalid.");
            }

            var result = await _koiService.UpdateKoiStatus(koiId, status);

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
        public async Task<IActionResult> GetAllKoiCategory()
        {
            var all = await _koiService.GetAllKoiCategory();
            return Ok(all);
        }
    }
}
