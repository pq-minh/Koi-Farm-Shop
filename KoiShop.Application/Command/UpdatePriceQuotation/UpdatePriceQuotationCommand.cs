using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdatePriceQuotation
{
    public  class UpdatePriceQuotationCommand : IRequest<Quotation>
    {
        public int QuotationId { get; set; }

        public int? RequestId { get; set; }
        public double? Price { get; set; }

        public string? Note { get; set; }

        public string? decision { get; set; }

    }
}
