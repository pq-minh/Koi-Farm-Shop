using KoiShop.Application.Queries.GetAllUser;
using KoiShop.Application.Queries.GetQuotation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/manageruser")]
    public class ManagerUserController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public  async Task<IActionResult> GetAllUser(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllUserQuery(pageNumber, pageSize);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
