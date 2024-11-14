using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetQuotation
{
    public class GetQuotationQuery : IRequest<PaginatedResult<QuotationWithKoi>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetQuotationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
