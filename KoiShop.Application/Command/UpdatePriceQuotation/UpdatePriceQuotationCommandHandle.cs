using AutoMapper;
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

namespace KoiShop.Application.Command.UpdatePriceQuotation
{
    public class UpdatePriceQuotationCommandHandle
        (IUserContext userContext,
        IUserStore<User> userStore,
        IQuotationRepository quotationRepository ,
        IMapper mapper) : IRequestHandler<UpdatePriceQuotationCommand, Quotation>
    {
        public async Task<Quotation> Handle(UpdatePriceQuotationCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var quotation = mapper.Map<Quotation>(request);
            var result = await quotationRepository.UpdatePriceQuotation(quotation, request.decision);
            return result;
        }
    }
}
