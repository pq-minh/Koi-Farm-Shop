using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KoiController : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    //var kois = await mediator.Send(new GetAllKoiQuery());
        //    //return Ok(kois);
        //}
        private readonly IKoiService _koiService;
        private readonly FirebaseService _firebaseService;

        public KoiController(IKoiService koiService, FirebaseService firebaseService)
        {
            _koiService = koiService;
            _firebaseService = firebaseService;
        }

        // Koi Methods =============================================================================================
        [HttpGet("get-koi")]
        public async Task<IActionResult> GetAllKoi()
        {
            var allKoi = await _koiService.GetAllKoi();
            if (allKoi == null) return NotFound();
            return Ok(allKoi);
        }

        [HttpGet("get-koi/{koiId}")]
        public async Task<IActionResult> GetKoiById(int koiId)
        {
            var koi = await _koiService.GetKoiById(koiId);
            return Ok(koi);
        }


        [HttpPost("add-koi")]
        public async Task<IActionResult> AddKoi([FromForm] AddKoiDto koiDto)
        {
            if (!await _koiService.ValidateAddKoiDtoInfo(koiDto))
            {
                return BadRequest("You have not entered koi information or the koi info is invalid.");
            }

            // upload ảnh lên firebase và trả về url ảnh
            var imageUrl = await _firebaseService.UploadFileToFirebaseStorageAsync(koiDto.ImageFile, "KoiFishImage");

            var koi = new Koi
            {
                FishTypeId = koiDto.FishTypeId,
                Name = koiDto.Name,
                Origin = koiDto.Origin,
                Description = koiDto.Description,
                Gender = koiDto.Gender,
                Image = imageUrl,
                Age = koiDto.Age,
                Weight = koiDto.Weight,
                Size = koiDto.Size,
                Personality = koiDto.Personality,
                Status = koiDto.Status,
                Price = koiDto.Price,
                Certificate = koiDto.Certificate
            };

            var addedKoi = await _koiService.AddKoi(koi);
            return Ok(addedKoi);
        }


        [HttpPut("update-koi/{koiId}")]
        public async Task<IActionResult> UpdateKoi(int koiId, [FromForm] UpdateKoiDto koi)
        {
            // lấy cá koi cần update từ database 
            var existingKoi = await _koiService.GetKoiById(koiId);
            if (existingKoi == null)
            {
                return NotFound();
            }

            // so sánh koi từ db vs koi gửi từ frontend
            // nếu property nào có tt hợp lệ gửi từ Form thì update, ko thì thôi
            if (koi.FishTypeId.HasValue)
            {
                if (await _koiService.ValidateFishTypeIdInKoi((int)koi.FishTypeId))
                {
                    existingKoi.FishTypeId = koi.FishTypeId;
                }
                else
                {
                    return BadRequest("Invalid FishTypeId");
                }
            }

            if (!string.IsNullOrEmpty(koi.KoiName)) existingKoi.Name = koi.KoiName;
            if (!string.IsNullOrEmpty(koi.Origin)) existingKoi.Origin = koi.Origin;
            if (!string.IsNullOrEmpty(koi.Description)) existingKoi.Description = koi.Description;
            if (!string.IsNullOrEmpty(koi.Gender)) existingKoi.Gender = koi.Gender;
            if (koi.Age.HasValue && koi.Age > 0) existingKoi.Age = koi.Age;
            if (koi.Weight.HasValue && koi.Weight > 0) existingKoi.Weight = koi.Weight;
            if (koi.Size.HasValue && koi.Size > 0) existingKoi.Size = koi.Size;
            if (!string.IsNullOrEmpty(koi.Personality)) existingKoi.Personality = koi.Personality;
            if (!string.IsNullOrEmpty(koi.Status)) existingKoi.Status = koi.Status;
            if (koi.Price.HasValue && koi.Price > 0) existingKoi.Price = koi.Price;
            if (!string.IsNullOrEmpty(koi.Certificate)) existingKoi.Certificate = koi.Certificate;

            // update image
            if (koi.ImageFile != null)
            {
                string oldImagePath = existingKoi.Image;

                // tách đg dẫn tuyệt đối thành đg dẫn tg đối  vì Firebase chỉ nhận vào đg dẫn tg đối
                var startIndex = oldImagePath.IndexOf("/o/") + 3; // 3 là độ dài của chuỗi "/o/"
                var endIndex = oldImagePath.IndexOf("?");
                var filePath = Uri.UnescapeDataString(oldImagePath.Substring(startIndex, endIndex - startIndex));

                // xóa ảnh cũ trong firebase
                var deleteSuccess = await _firebaseService.DeleteFileInFirebaseStorageAsync(filePath);
                if (!deleteSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Cannot delete old image.");
                }

                // tải ảnh mới lên firebase
                existingKoi.Image = await _firebaseService.UploadFileToFirebaseStorageAsync(koi.ImageFile, "KoiFishImage");
            }

            // lưu thay đổi vào database
            await _koiService.UpdateKoi(existingKoi);

            return Ok(existingKoi);
        }




        // KoiCategory Methods ======================================================================================
        [HttpGet("get-koi-category")]
        public async Task<IActionResult> GetAllKoiCategory()
        {
            var allKoiCategory = await _koiService.GetAllKoiCategory();
            return Ok(allKoiCategory);
        }

        [HttpPost("add-koi-category")]
        public async Task<IActionResult> AddKoiCategory([FromBody] KoiCategory koiCategory)
        {
            var addedCategory = await _koiService.AddKoiCategory(koiCategory);
            return Ok(addedCategory);
        }

        [HttpPut("update-koi-category")]
        public async Task<IActionResult> UpdateKoiCategory([FromBody] KoiCategory koiCategory)
        {
            var updatedCategory = await _koiService.UpdateKoiCategory(koiCategory);
            return Ok(updatedCategory);
        }

        [HttpDelete("delete-koi-category")]
        public async Task<IActionResult> DeleteKoiCategory([FromBody] KoiCategory koiCategory)
        {
            if (koiCategory == null || koiCategory.FishTypeId <= 0)
            {
                return BadRequest("Invalid KoiCategory");
            }

            var koiList = await _koiService.GetKoiInKoiCategory(koiCategory.FishTypeId);

            if (koiList.Any())
            {
                return Conflict("Cannot delete KoiCategory because it contains Koi");
            }

            var result = await _koiService.DeleteKoiCategory(koiCategory.FishTypeId);

            if (!result)
            {
                return NotFound("KoiCategory not found");
            }

            return Ok("KoiCategory deleted successfully");
        }
    }
}
