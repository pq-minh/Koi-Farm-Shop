using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.CreateDiscount
{
    public class CreateDiscountCommandHandle
        ( IDiscountRepository discountRepository): IRequestHandler<CreateDiscountCommand, string>
    {
        public Task<string> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountCreate = new Discount
            {
                Name = request.Name,
                TotalQuantity = request.TotalQuantity,
                Description = request.Description,
                DiscountRate = request.DiscountRate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            var result = discountRepository.CreateDiscount(discountCreate);
            return result;
        }
    }
}
