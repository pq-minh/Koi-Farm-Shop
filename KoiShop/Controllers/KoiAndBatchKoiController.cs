using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [Route("api/KoiAndBatchKoi")]
    [ApiController]
    public class KoiAndBatchKoiController : ControllerBase
    {
        private readonly IKoiAndBatchKoiService _koiAndBatchKoiService;

        public KoiAndBatchKoiController(IKoiAndBatchKoiService koiAndBatchKoiService)
        {
            _koiAndBatchKoiService = koiAndBatchKoiService;
        }
        [HttpGet("GetAllKoiAndBatch")]
        public async Task<IActionResult> GetAllKoiAndBatch([FromQuery] KoiFilterDto koiFilterDto)
        {
            var allKoiAndBatch = await _koiAndBatchKoiService.GetAllKoiAndBatch(koiFilterDto);
            return Ok(allKoiAndBatch);
        }
    }
}
