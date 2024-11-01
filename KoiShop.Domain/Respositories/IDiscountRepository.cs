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
        Task<Discount> UpdateDiscount(Discount discount);
        Task<Discount> CreateDiscount(Discount discount);
        Task<IEnumerable<Discount>> GetDiscount();
        Task<IEnumerable<Discount>> GetDiscountForUser();
        Task<Discount?> GetDiscountForUser(string? name);
        Task<double> CheckDiscount(int? disountId);
        Task<bool> UpdateDiscountStatus(int discountId);
    }
}
