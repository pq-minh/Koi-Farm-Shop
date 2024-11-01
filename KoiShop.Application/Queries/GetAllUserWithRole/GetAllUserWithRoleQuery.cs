using KoiShop.Application.Dtos.Pagination;
using KoiShop.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetAllUserWithRole
{
    public class GetAllUserWithRoleQuery : IRequest<PaginatedResult<UserDtoWithRole>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllUserWithRoleQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
