using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate);
    }

}
