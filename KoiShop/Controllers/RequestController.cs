using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using KoiShop.Infrastructure.Persistence;
using KoiShop.Application.Request.Dtos;
using KoiShop.Domain.Entities;
using Request = KoiShop.Domain.Entities.Request;
using KoiShop.Application.Koi.Dtos;
using static KoiShop.Application.Users.UserContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KoiShop.Application.Package.Dtos;
using KoiShop.Application.Quotation.Dtos;
using KoiShop.Application.ViewModel.KoiPackageViewModel;
namespace KoiShop.Controllers
{

    [ApiController]
    [Route("api/request")]
    public class RequestController(KoiShopV1DbContext koiShopV1DbContext , IUserContext userContext, IUserStore<User> userStore) : ControllerBase
    {


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CreatePackage")]
        public async Task<IActionResult> CreatePackage([FromBody] KoiDto koiDto, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            var koi = new Koi()
            {
                UserId = user.Id,
                User = dbUser,
                Name = koiDto.Name,
                Origin = koiDto.Origin,
                Description = koiDto.Description,
                Gender = koiDto.Gender,
                Image = koiDto.Image,
                Age = koiDto.Age,
                Weight = koiDto.Weight,
                Size = koiDto.Size,
                Personality = koiDto.Personality,
                Status = koiDto.Status,
                FishTypeId = koiDto.FishTypeId,
                Packages = koiDto.Packages.Select(pkg => new Package
                {
                    Quantity = pkg.Quantity,
                    BatchKoiId = null, 
                    Requests = pkg.Requests.Select(req => new Request
                    {
                        UserId = req.UserId,
                        User = dbUser,
                        CreatedDate = req.CreatedDate,
                        RelationalRequestId = req.RelationalRequestId,
                        ConsignmentDate = req.ConsignmentDate,
                        EndDate = req.EndDate,
                        AgreementPrice = req.AgreementPrice,
                        TypeRequest = req.TypeRequest,
                    }).ToList()
                }).ToList()
            };

            await koiShopV1DbContext.Kois.AddAsync(koi, cancellationToken);
            await koiShopV1DbContext.SaveChangesAsync(cancellationToken);

            return Ok(koi);
        }

    }
}
