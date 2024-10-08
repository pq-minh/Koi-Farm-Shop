using KoiShop.Application.Interfaces;
using KoiShop.Application.Queries.GetAllKoi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/kois")]
    public class KoiController: ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var kois = await mediator.Send(new GetAllKoiQuery());
        //    return Ok(kois);
        //}
        private readonly IKoiService _koiService;
        public KoiController(IKoiService koiService)
        {
            _koiService = koiService;
        }

        [HttpGet("GetAllKoi")]
        public async Task<IActionResult> GetKoi()
        {
            var allKoi = await _koiService.GetAllKoi();
            return Ok(allKoi);
        }
        [HttpGet("GetAllKoiWithCondition")]  //có 2 cách viết thêm tham số 
                                             //1. HttpGet("GetAllKoiWithCondition/{KoiName}")
                                             //2.
        public async Task<IActionResult> GetKoiWithCondition(string? koiName, double? from, double? to, string? sortBy, int pageNumber, int pageSize)
        {
            var allKoiWithCondition = await _koiService.GetAllKoiWithCondition(koiName, from, to, sortBy, pageNumber, pageSize);
            return Ok(allKoiWithCondition);
        }
        
    }
}
