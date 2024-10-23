using KoiShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDto>> GetDiscount();
        Task<IEnumerable<DiscountDto>> GetDiscountForUser();
        Task<DiscountDto> GetDiscountForUser(string? name);
    }
}
