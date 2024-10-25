using AutoMapper;
using KoiShop.Application.Dtos.Pagination;
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
        IMapper mapper) : IRequestHandler<GetAllRequestQuery, PaginatedResult<RequestDtoResponse>>
    {
        public async Task<PaginatedResult<RequestDtoResponse>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            var requests = await requestRepository.GetAllRequest(user.Id, pageNumber, pageSize);
            var requestDtos = mapper.Map<PaginatedResult<RequestDtoResponse>>(requests);
            return requestDtos;
        }
    }
}
