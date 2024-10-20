using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    public class DiscountRepository(KoiShopV1DbContext koiShopV1DbContext) :IDiscountRepository
    {
        public async Task<string> UpdateDiscount(Discount discount)
        {
            var discountResult = koiShopV1DbContext.Discounts.FirstOrDefault(dc => dc.DiscountId == discount.DiscountId);
            if (discountResult != null)
            {
                discountResult.TotalQuantity = discount.TotalQuantity;
                discountResult.EndDate = discount.EndDate;
                discountResult.StartDate = discount.StartDate;
                discountResult.DiscountRate = discount.DiscountRate;
                await koiShopV1DbContext.SaveChangesAsync();
                return "Cập nhật thành công";
            }
            return "Không tìm thấy mã giảm giá";
            
        }

        public async Task<string> CreateDiscount(Discount discount)
        {   
            if (discount.Name != null && koiShopV1DbContext.Discounts.Any(dc => dc.Name == discount.Name))
            {
                return "Tên của mã giảm giá đã tồn tại";
            }
            var discountCreate = await koiShopV1DbContext.Discounts.AddAsync(discount);
            await koiShopV1DbContext.SaveChangesAsync();
            return "Tạo mã giảm giá thành công";
        }
    }
}
