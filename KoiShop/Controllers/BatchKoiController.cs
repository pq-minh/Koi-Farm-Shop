using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Dtos;
using KoiShop.Application.Service;

namespace KoiShop.Controllers
{
    
    [ApiController]
    [Route("/api/BatchKoi")]
    public class BatchKoiController : ControllerBase
    {
        private readonly IBatchKoiService _batchKoiService;
        public BatchKoiController(IBatchKoiService batchKoiService)
        {
            _batchKoiService = batchKoiService;
        }
        [HttpGet("GetBatchKoi")]
        public async Task<IActionResult> GetBatchKoi()
        {
            var batchKoi = await _batchKoiService.GetAllBatchKoi();
            return Ok(batchKoi);
        }
        [HttpGet("GetAllBatchKoiWithCondition")]  
        public async Task<IActionResult> GetKoiWithCondition([FromQuery] KoiFilterDto koiFilterDto)
        {
            var allKoiWithCondition = await _batchKoiService.GetAllBatchKoiWithCondition(koiFilterDto);
            return Ok(allKoiWithCondition);
        }
    }
}
