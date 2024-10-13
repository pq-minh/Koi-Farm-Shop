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
    public class OrderRepository : IOrderRepository
    {
        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public OrderRepository(KoiShopV1DbContext KoiShopV1DbContext)
        {
            _KoiShopV1DbContext = KoiShopV1DbContext;
        }

        public async Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _KoiShopV1DbContext.Orders
                .Where(o => o.OrderStatus == "Completed" &&
                            o.CreateDate.HasValue &&
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .ToListAsync();
        }

    }
}
