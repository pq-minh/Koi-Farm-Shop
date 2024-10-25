using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Application.Dtos.RequestDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<PaginatedResult<UserDtoV1>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllUserQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
