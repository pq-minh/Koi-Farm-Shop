using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Infrastructure.Respositories
{
    public class DiscountRepository :IDiscountRepository
    {
        private readonly KoiShopV1DbContext _koiShopV1DbContext;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        public DiscountRepository(KoiShopV1DbContext koiShopV1DbContext, IUserStore<User> userStore, IUserContext userContext)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userStore = userStore;
            _userContext = userContext;
        }

        public async Task<string> UpdateDiscount(Discount discount)
        {
            var discountResult = _koiShopV1DbContext.Discounts.FirstOrDefault(dc => dc.DiscountId == discount.DiscountId);
            if (discountResult != null)
            {
                discountResult.TotalQuantity = discount.TotalQuantity;
                discountResult.EndDate = discount.EndDate;
                discountResult.StartDate = discount.StartDate;
                discountResult.DiscountRate = discount.DiscountRate;
                await _koiShopV1DbContext.SaveChangesAsync();
                return "Cập nhật thành công";
            }
            return "Không tìm thấy mã giảm giá";
            
        }

        public async Task<string> CreateDiscount(Discount discount)
        {   
            if (discount.Name != null && _koiShopV1DbContext.Discounts.Any(dc => dc.Name == discount.Name))
            {
                return "Tên của mã giảm giá đã tồn tại";
            }
            var discountCreate = await _koiShopV1DbContext.Discounts.AddAsync(discount);
            await _koiShopV1DbContext.SaveChangesAsync();
            return "Tạo mã giảm giá thành công";
        }

        public async Task<IEnumerable<Discount>> GetDiscount()
        {
            var discount = await _koiShopV1DbContext.Discounts.Where(d => d.TotalQuantity > 0 && d.StartDate <= DateTime.Now && DateTime.Now <= d.EndDate).ToListAsync();
            return discount;
        }
        public async Task<IEnumerable<Discount>> GetDiscountForUser()
        {
            var userId = _userContext.GetCurrentUser();
            if (userId == null)
            {
                return Enumerable.Empty<Discount>();
            }
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId.Id).Select(o => o.DiscountId).ToListAsync();
            var validDiscounts = await _koiShopV1DbContext.Discounts.Where(d => d.TotalQuantity > 0 && d.StartDate <= DateTime.Now && DateTime.Now <= d.EndDate).ToListAsync();
            if (order != null)
            {
                var availableDiscount = validDiscounts.Where(d => !order.Contains(d.DiscountId)).ToList();
                return availableDiscount;
            }
            else
            {
                return validDiscounts;
            }
        }
        public async Task<Discount?> GetDiscountForUser(string? name)
        {
            var userId = _userContext.GetCurrentUser();
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId.Id).Select(o => o.DiscountId).ToListAsync();
            var validDiscounts = await _koiShopV1DbContext.Discounts.Where(d => d.TotalQuantity > 0 && d.StartDate <= DateTime.Now && DateTime.Now <= d.EndDate && d.Name == name).ToListAsync();
            if (order != null)
            {
                var availableDiscount = validDiscounts.FirstOrDefault(d => !order.Contains(d.DiscountId));
                return availableDiscount;
            }
            else
            {
                return null;
            }
        }
        public async Task<double> CheckDiscount(int? disountId)
        {
            if (disountId == null || disountId == 0)
            {
                return (double)0;
            }
            var userId = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId.Id).Select(o => o.DiscountId).ToListAsync();
            var discount = await _koiShopV1DbContext.Discounts.Where(d => d.DiscountId == disountId).FirstOrDefaultAsync();

            if (order != null)
            {
                if (discount != null)
                {
                    if (discount.StartDate <= DateTime.Now && discount.EndDate >= DateTime.Now && !order.Contains(discount.DiscountId) && discount.TotalQuantity > 0 && discount.Used <= discount.TotalQuantity)
                    {
                        var pricePercent = (double)discount.DiscountRate;
                        discount.Used++;
                        _koiShopV1DbContext.Discounts.Update(discount);
                        await _koiShopV1DbContext.SaveChangesAsync();
                        return pricePercent;
                    }
                }
                else
                {
                    return (double)0;
                }

            }
            return (double)0;
        }
    }
}
