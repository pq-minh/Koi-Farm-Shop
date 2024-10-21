using AutoMapper;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Queries.GetQuotation
{
    public class GetQuotationQueryHandle
         (IUserContext userContext,
        IUserStore<User> userStore,
        IQuotationRepository quotationRepository,
        IMapper mapper)
        : IRequestHandler<GetQuotationQuery, PaginatedResult<QuotationWithKoi>>
    {
        public async Task<PaginatedResult<QuotationWithKoi>> Handle(GetQuotationQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            int pageNumber = request.PageNumber; 
            int pageSize = request.PageSize; 

            var quotations = await quotationRepository.GetQuotation(user.Id, pageNumber, pageSize);
            return quotations;
        }
    }
}
