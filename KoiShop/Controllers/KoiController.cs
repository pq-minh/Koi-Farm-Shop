
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/kois")]
    public class KoiController(IMediator mediator) : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    //var kois = await mediator.Send(new GetAllKoiQuery());
        //    //return Ok(kois);
        //}
    }
}
