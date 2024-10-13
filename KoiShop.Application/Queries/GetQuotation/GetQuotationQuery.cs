using KoiShop.Domain.Constant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetQuotation
{
    public class GetQuotationQuery : IRequest<IEnumerable<QuotationWithKoi>>
    {

    }
}
