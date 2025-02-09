using Datadog.Trace;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Queries.GetAllKoi;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/kois")]
    public class KoiController : ControllerBase
    {
        private readonly IKoiService _koiService;
        private readonly IFishCachingService _fishCachingService;

        public KoiController(IKoiService koiService, IFishCachingService fishCachingService)
        {
            _koiService = koiService;
            _fishCachingService = fishCachingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetKois()
        {
                var allKoi = await _fishCachingService.GetFishOnSale();
                return Ok(allKoi);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetKoi(int id)
        {
            var koi = await _fishCachingService.GetFishIdOnsale(id);
            return Ok(koi);
        }
        [HttpGet("modify")]  //có 2 cách viết thêm tham số 
                                             //1. HttpGet("GetAllKoiWithCondition/{KoiName}")
                                             //2. [FromQuery] string koiName
        public async Task<IActionResult> GetKoiWithCondition([FromQuery] KoiFilterDto koiFilterDto)
        {
            var allKoiWithCondition = await _koiService.GetAllKoiWithCondition(koiFilterDto);
            return Ok(allKoiWithCondition);
        }
        
    }
}
