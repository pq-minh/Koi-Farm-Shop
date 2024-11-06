using KoiShop.Application.Command.CreatePost;
using KoiShop.Application.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/post")]
    public class PostController(IMediator mediator) : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("createpost")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand model)
        {
            var result= await mediator.Send(model);
            return Ok(result);
        }
    }
}
