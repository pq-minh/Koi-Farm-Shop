using AutoMapper;
using KoiShop.Application.Dtos.RequestDtos;
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

namespace KoiShop.Application.Queries.GetAllRequest
{
    public class GetAllRequestQueryHandle(IUserContext userContext,
        IUserStore<User> userStore,
        IRequestRepository requestRepository,
        IMapper mapper) : IRequestHandler<GetAllRequestQuery, IEnumerable<RequestDtoResponse>>
    {
        public async Task<IEnumerable<RequestDtoResponse>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var requests = await requestRepository.GetAllRequest(user.Id);
            var requestDtos = mapper.Map<IEnumerable<RequestDtoResponse>>(requests);
            return requestDtos;
        }
    }
}
