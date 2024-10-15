using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Dtos;
using KoiShop.Application.Service;

namespace KoiShop.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]s")]
    public class BatchKoiController : ControllerBase
    {
        private readonly IBatchKoiService _batchKoiService;
        public BatchKoiController(IBatchKoiService batchKoiService)
        {
            _batchKoiService = batchKoiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBatchKoi()
        {
            var batchKoi = await _batchKoiService.GetAllBatchKoi();
            return Ok(batchKoi);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetKoi(int id)
        {
            var koi = await _batchKoiService.GetBatchKoi(id);
            return Ok(koi);
        }
        [HttpGet("modify")]  
        public async Task<IActionResult> GetKoiWithCondition([FromQuery] KoiFilterDto koiFilterDto)
        {
            var allKoiWithCondition = await _batchKoiService.GetAllBatchKoiWithCondition(koiFilterDto);
            return Ok(allKoiWithCondition);
        }
    }
}
