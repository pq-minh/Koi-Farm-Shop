using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate);
    }

}
