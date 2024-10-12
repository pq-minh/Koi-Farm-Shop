
using KoiShop.Application.Users;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Command.CreateRequest
{
    public class CreateRequestCommandHandle(
        IUserContext userContext,
        IUserStore<User> userStore,
        IRequestRepository requestRepository
        ) : IRequestHandler<CreateRequestCommand, Koi>
    {
        public async Task<Koi> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {

            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            var koi = new Koi()
            {
                UserId = user.Id,
                User = dbUser,
                Name = request.Name,
                Origin = request.Origin,
                Description = request.Description,
                Gender = request.Gender,
                Image = request.Image,
                Age = request.Age,
                Weight =  request.Weight,
                Size = request.Size,
                Personality = request.Personality,
                Status = request.Status,
                FishTypeId = request.FishTypeId,
                Packages = request.Packages.Select(pkg => new Package
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
                        Status = req.Status,
                        Quotations = req.Quotations.Select(qt => new Quotation
                        {
                            CreateDate = req.CreatedDate,
                            Price = qt.Price,
                            Status = qt.Status,
                        }).ToList()                       
                    }).ToList()
                }).ToList()
            };
           var result = await requestRepository.CreateRequest(koi);
              return result;
        }
    }
}
