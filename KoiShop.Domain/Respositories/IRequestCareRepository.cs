using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IRequestCareRepository
    {
        Task<IEnumerable<OrderDetail>> GetCurrentOrderdetail();
        Task<IEnumerable<OrderDetail>> GetAllOrderDetail();
        Task<IEnumerable<Request>> GetAllRequestCareByStaff();
        Task<IEnumerable<Request>> GetAllRequestCareByCustomer();
        Task<bool> AddKoiOrBatchToPackage(List<OrderDetail> orderDetails);
        Task<bool> AddKoiOrBatchToRequest(List<OrderDetail> orderDetails, DateTime endDate);
        Task<bool> CheckRequest(int? id, string? status);
        Task<bool> UpdateKoiOrBatchToCare(int? id, string? status);
    }
}
