using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdateDiscount
{
    public class UpdateDiscountCommand : IRequest<Discount>
    {
            public int DiscountId { get; set; }
            public double? DiscountRate { get; set; }
            public int? TotalQuantity { get; set; }
            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }
    }
}
