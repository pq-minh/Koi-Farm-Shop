using KoiShop.Application.Dtos;
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
        Task<OrderEnum> AddOrders(List<CartDtoV2> carts, string method, int? discountId, string? phoneNumber, string? address);
        Task<IEnumerable<OrderDetailDtos>> GetOrderDetail();
        Task<IEnumerable<OrderDtos>> GetOrder();
        Task<IEnumerable<OrderDetailDtos>> GetOrderDetailById(int? id);
        Task<IEnumerable<KoiDto>> GetKoiFromOrderDetail(int? orderId);
        Task<IEnumerable<BatchKoiDto>> GetBatchFromOrderDetail(int? orderId);
    }
}
