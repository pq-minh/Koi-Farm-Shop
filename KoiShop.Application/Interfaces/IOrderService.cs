using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.OrderDtos;
using KoiShop.Application.Dtos.VnPayDtos;
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
        Task<OrderEnum> AddOrders(List<CartDtoV2> carts, string method, int? discountId, string? phoneNumber, string? address, VnPaymentResponseFromFe request);
        Task<IEnumerable<OrderDetailDtos>> GetOrderDetail();
        Task<IEnumerable<OrderDtos>> GetOrder();
        Task<IEnumerable<OrderDetailDtoV2>> GetOrderDetailById(int? id);
        Task<IEnumerable<KoiDto>> GetKoiFromOrderDetail(int? orderId);
        Task<IEnumerable<BatchKoiDto>> GetBatchFromOrderDetail(int? orderId);

        // ===============================================================================================
        Task<IEnumerable<Order>> GetOrders(string status, DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderDetail>> GetOrderDetails(string status, DateTime startDate, DateTime endDate);
        Task<int> GetBestSalesKoi(DateTime startDate, DateTime endDate);
        Task<int> GetBestSalesBatchKoi(DateTime startDate, DateTime endDate);
        Task<int> GetTotalOrders(DateTime startDate, DateTime endDate);
        Task<int> GetCompletedOrders(DateTime startDate, DateTime endDate);
        Task<int> GetPendingOrders(DateTime startDate, DateTime endDate);
        Task<PaymentDto> PayByVnpay(VnPaymentResponseFromFe request);
        Task<bool> UpdateOrder(UpdateOrderDtos order);
        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
