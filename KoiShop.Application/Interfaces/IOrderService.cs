using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate);
    }

}
