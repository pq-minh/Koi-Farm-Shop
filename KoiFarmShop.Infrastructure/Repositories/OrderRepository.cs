using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Domain.Interfaces;
using KoiFarmShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly KoiFarmShopContext _context;

        public OrderRepository(KoiFarmShopContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate)
        {

            return await _context.Orders
                .Where(o => o.OrderStatus == "Completed" &&
                            o.CreateDate.HasValue &&  
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .ToListAsync();
        }

    }
}
