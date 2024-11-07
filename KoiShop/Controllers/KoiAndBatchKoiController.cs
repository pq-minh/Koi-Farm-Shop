using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [Route("api/koisandbatchkois")]
    [ApiController]
    public class KoiAndBatchKoiController : ControllerBase
    {
        private readonly IKoiAndBatchKoiService _koiAndBatchKoiService;

        public KoiAndBatchKoiController(IKoiAndBatchKoiService koiAndBatchKoiService)
        {
            _koiAndBatchKoiService = koiAndBatchKoiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllKoiAndBatch([FromQuery] KoiFilterDto koiFilterDto)
        {
            var allKoiAndBatch = await _koiAndBatchKoiService.GetAllKoiAndBatch(koiFilterDto);
            return Ok(allKoiAndBatch);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAllKoiAndBatchId([FromQuery] KoiAndBathKoiId koiAndBatchKoiIdDto)
        {
            var allKoiAndBatch = await _koiAndBatchKoiService.GetKoiOrBatchSold(koiAndBatchKoiIdDto.KoiId, koiAndBatchKoiIdDto.BathcKoiId);
            return Ok(allKoiAndBatch);
        }
    }
}
