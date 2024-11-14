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
        ( IDiscountRepository discountRepository): IRequestHandler<CreateDiscountCommand, Discount>
    {
        public Task<Discount> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountCreate = new Discount
            {
                Name = request.Name,
                TotalQuantity = request.TotalQuantity,
                Description = request.Description,
                DiscountRate = request.DiscountRate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Used = 0,
                Status ="Active"
            };
            var result = discountRepository.CreateDiscount(discountCreate);
            if(result == null)
            {
                throw new Exception("Name of discount is duplicated");
            }
            return result;
        }
    }
}
