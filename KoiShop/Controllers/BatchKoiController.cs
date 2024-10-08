using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShop.Application.Interfaces;

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
            var batchKoi = _batchKoiService.GetAllBatchKoi();
            return Ok(batchKoi);
        }
    }
}
