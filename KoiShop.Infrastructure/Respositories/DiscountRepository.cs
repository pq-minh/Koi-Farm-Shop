using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    public class DiscountRepository : IDiscountRepository
    {

        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public DiscountRepository(KoiShopV1DbContext KoiShopV1DbContext)
        {
            _KoiShopV1DbContext = KoiShopV1DbContext;
        }

        public async Task<bool> AddDiscount(Discount discount)
        {
            _KoiShopV1DbContext.Discounts.Add(discount);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscounts()
        {
            return await _KoiShopV1DbContext.Discounts.ToListAsync();
        }

        public async Task<Discount> GetDiscountById(int id)
        {
            return await _KoiShopV1DbContext.Discounts.FindAsync(id);
        }

        public async Task<bool> UpdateDiscount(Discount discount)
        {
            _KoiShopV1DbContext.Discounts.Update(discount);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
