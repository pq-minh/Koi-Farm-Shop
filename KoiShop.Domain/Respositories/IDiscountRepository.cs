using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IDiscountRepository
    {
        Task<string> UpdateDiscount(Discount discount);
        Task<string> CreateDiscount(Discount discount);
    }
}
