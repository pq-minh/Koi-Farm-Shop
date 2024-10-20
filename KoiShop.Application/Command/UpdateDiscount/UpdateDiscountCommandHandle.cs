using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdateDiscount
{
    public class UpdateDiscountCommandHandle(
        IDiscountRepository discountRepository) : IRequestHandler<UpdateDiscountCommand, string>
    {
        public async Task<string> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountUpdate = new Discount
            {
                DiscountId = request.DiscountId,
                DiscountRate = request.DiscountRate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalQuantity = request.TotalQuantity,
            };
           var result = await discountRepository.UpdateDiscount(discountUpdate);
           return result;
        }
    }
}
