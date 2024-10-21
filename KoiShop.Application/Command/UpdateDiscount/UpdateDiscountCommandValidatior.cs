using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdateDiscount
{
    public class UpdateDiscountCommandValidatior :AbstractValidator<UpdateDiscountCommand>

    {
        public UpdateDiscountCommandValidatior()
        {
            RuleFor(dto => dto.DiscountRate).NotEmpty().LessThan(100).GreaterThan(5);
            RuleFor(dto => dto.StartDate).NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow);
           RuleFor(dto => dto.TotalQuantity).NotEmpty().GreaterThanOrEqualTo(5).LessThanOrEqualTo(300);
        }
    }
}
