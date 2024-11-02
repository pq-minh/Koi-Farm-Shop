using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(dto => dto.DiscountRate).NotEmpty().LessThan(100).GreaterThan(5);
            RuleFor(dto => dto.StartDate).NotEmpty().LessThan(dto => dto.EndDate);
            RuleFor(dto => dto.TotalQuantity).NotEmpty().GreaterThanOrEqualTo(5).LessThanOrEqualTo(300);
        }
    }
}
