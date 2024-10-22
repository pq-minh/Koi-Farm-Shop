using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Application.Dtos.RequestDtos;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetAllUser
{
    public class GetAllUserQueryHandle
        (IUserRepository userRepository): IRequestHandler<GetAllUserQuery, PaginatedResult<UserDtoV1>>
    {
        public async Task<PaginatedResult<UserDtoV1>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var userResult = await userRepository.GetAllUser(request.PageNumber,request.PageSize);
            var userDtos = userResult.Items.Select(user => new UserDtoV1
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Point   = user.Point,
                Status = user.Status,

            }).ToList();
            return new PaginatedResult<UserDtoV1>(userDtos, userResult.TotalCount,request.PageNumber,request.PageSize);
        }
    }
}
