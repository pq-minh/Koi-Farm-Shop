using KoiShop.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchKoiController : ControllerBase
    {
        private readonly BatchKoiService _batchKoiService;

        public BatchKoiController(BatchKoiService batchKoiService)
        {
            _batchKoiService = batchKoiService;
        }


        //public async Task<IActionResult> GetAllBatchKoi()
        //{
        //    var  allBatchKoi = await _batchKoiService.GetAllBatchKoi();
        //    return Ok(allBatchKoi);
        //}


        //// Thêm BatchKoi
        //[HttpPost("add-batchkoi")]
        //public async Task<IActionResult> AddBatchKoi([FromBody] BatchKoi batchKoi)
        //{
        //    var addedBatchKoi = await _batchKoiService.AddBatchKoi(batchKoi);
        //    return Ok(addedBatchKoi);
        //}

        //// Cập nhật BatchKoi
        //[HttpPut("update-batchkoi")]
        //public async Task<IActionResult> UpdateBatchKoi([FromBody] BatchKoi batchKoi)
        //{
        //    var updatedBatchKoi = await _batchKoiService.UpdateBatchKoi(batchKoi);
        //    return Ok(updatedBatchKoi);
        //}

        //// Xóa BatchKoi
        //[HttpDelete("delete-batchkoi/{id}")]
        //public async Task<IActionResult> DeleteBatchKoi(int id)
        //{
        //    var result = await _batchKoiService.DeleteBatchKoi(id);
        //    if (!result)
        //    {
        //        return NotFound("BatchKoi not found");
        //    }
        //    return Ok("BatchKoi deleted successfully");
        //}

        //// Lấy BatchKoi theo ID
        //[HttpGet("get-batchkoi/{id}")]
        //public async Task<IActionResult> GetBatchKoiById(int id)
        //{
        //    var batchKoi = await _batchKoiService.GetBatchKoiById(id);
        //    if (batchKoi == null)
        //    {
        //        return NotFound("BatchKoi not found");
        //    }
        //    return Ok(batchKoi);
        //}
    }

}
