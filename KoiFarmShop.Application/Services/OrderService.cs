using KoiFarmShop.Application.Interfaces;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetCompletedOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetCompletedOrdersAsync(startDate, endDate);
        }
    }
}
