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
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Infrastructure.Respositories
{
    public class RequestCareRepository : IRequestCareRepository
    {
        private readonly KoiShopV1DbContext _koiShopV1DbContext;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        public RequestCareRepository(KoiShopV1DbContext koiShopV1DbContext, IUserStore<User> userStore, IUserContext userContext)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userStore = userStore;
            _userContext = userContext;
        }
        public async Task<IEnumerable<OrderDetail>> GetCurrentOrderdetail()
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == user.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            var request = await _koiShopV1DbContext.Requests.Where(r => r.UserId == user.Id).Include(r => r.Package).ToListAsync();
            if (request == null || !request.Any())
            {
                var orderdetails = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId)
                    .Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
                if (orderdetails == null)
                {
                    return Enumerable.Empty<OrderDetail>();
                }
                return orderdetails;
            }
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId && !request.Any(r => r.Package.KoiId == od.KoiId || r.Package.BatchKoiId == od.BatchKoiId)).
               Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            return orderDetail;
        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail()
        {
            var userId = _userContext.GetCurrentUser().Id;

            var request = await _koiShopV1DbContext.Requests.Where(r => r.UserId == userId).Include(r => r.Package).ToListAsync();
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).Select(o => o.OrderId).ToListAsync();
            var completedPaymentIds = await _koiShopV1DbContext.Payments.Where(p => order.Contains((int)p.OrderId) && p.Status == "Completed").Select(p => p.OrderId).ToListAsync();
            if (order == null || completedPaymentIds == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            if (request == null || request.Count == 0)
            {
                var orderDetails = await _koiShopV1DbContext.OrderDetails.Where(od => completedPaymentIds.Contains((int)od.OrderId)
                && od.Status == "Pending")
                    .Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
                if (orderDetails == null)
                {
                    return Enumerable.Empty<OrderDetail>();
                }
                return orderDetails;
            }
            var koiIdsFromRequests = await _koiShopV1DbContext.Requests
                .Where(r => r.UserId == userId && r.Package.KoiId.HasValue && !r.Package.BatchKoiId.HasValue)
                .Select(r => r.Package.KoiId.Value)
                .ToListAsync();

            var batchKoiIdsFromRequests = await _koiShopV1DbContext.Requests
                .Where(r => r.UserId == userId && r.Package.BatchKoiId.HasValue && !r.Package.KoiId.HasValue)
                .Select(r => r.Package.BatchKoiId.Value)
                .ToListAsync();

            var orderDetail = await _koiShopV1DbContext.OrderDetails
                .Where(od => completedPaymentIds.Contains((int)od.OrderId) &&
                             !(koiIdsFromRequests.Contains((int)od.KoiId) || batchKoiIdsFromRequests.Contains((int)od.BatchKoiId))
                             && od.Status == "Pending")
                .Include(od => od.Koi)
                .Include(od => od.BatchKoi)
                .ToListAsync();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            return orderDetail;
        }
        public async Task<IEnumerable<Request>> GetAllRequestCareByCustomer()
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null)
            {
                return Enumerable.Empty<Request>();
            }
            var request = await _koiShopV1DbContext.Requests.Where(r => r.UserId == user.Id).Include(r => r.Package).Include(r => r.Package.Koi).Include(r => r.Package.BatchKoi).ToListAsync();
            if (request == null)
            {
                return Enumerable.Empty<Request>();
            }
            return request;
        }
        public async Task<IEnumerable<Request>> GetAllRequestCareByStaff()
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null)
            {
                return Enumerable.Empty<Request>();
            }
            var request = await _koiShopV1DbContext.Requests.Include(r => r.Package).Include(r => r.Package.Koi).
                Include(r => r.Package.BatchKoi).ToListAsync();
            if (request == null)
            {
                return Enumerable.Empty<Request>();
            }
            return request;
        }
        public async Task<bool> AddKoiOrBatchToPackage(List<OrderDetail> orderDetails)
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null || orderDetails == null)
            {
                return false;
            }
            foreach (var order in orderDetails)
            {
                var package = new Package
                {
                    BatchKoiId = order.BatchKoiId,
                    KoiId = order.KoiId,
                    Quantity = order.ToTalQuantity
                };
                _koiShopV1DbContext.Packages.Add(package);
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddKoiOrBatchToRequest(List<OrderDetail> orderDetails, DateTime endDate)
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null || orderDetails == null)
            {
                return false;
            }
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == user.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            //var orderdetailKoiId = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).Select(od => od.KoiId).ToListAsync();
            //var orderdetailBatchKoiId = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).Select(od => od.BatchKoiId).ToListAsync();
            var orderdetailKoiId = orderDetails.Where(od => od.KoiId.HasValue && od.BatchKoiId == null).Select(od => od.KoiId);
            var orderdetailBatchKoiId = orderDetails.Where(od => od.BatchKoiId.HasValue && od.KoiId == null).Select(od => od.BatchKoiId);
            var packageKoiId = await _koiShopV1DbContext.Packages.Where(p => orderdetailKoiId.Contains(p.KoiId)).ToListAsync();
            var packageBatchKoiId = await _koiShopV1DbContext.Packages.Where(p => orderdetailBatchKoiId.Contains(p.BatchKoiId)).
                ToListAsync();

            foreach (var package in packageKoiId)
            {
                var request = new Request
                {
                    CreatedDate = DateTime.Now,
                    PackageId = package.PackageId,
                    ConsignmentDate = DateTime.Now,
                    TypeRequest = "Care",
                    EndDate = endDate,
                    UserId = user.Id,
                    Status = "Pending"
                };
                _koiShopV1DbContext.Requests.Add(request);
            }

            foreach (var package in packageBatchKoiId)
            {
                var request = new Request
                {
                    CreatedDate = DateTime.Now,
                    ConsignmentDate = DateTime.Now,
                    PackageId = package.PackageId,
                    TypeRequest = "Care",
                    EndDate = endDate,
                    UserId = user.Id,
                    Status = "Pending"
                };
                _koiShopV1DbContext.Requests.Add(request);
            }
            foreach (var orderDetail in orderDetails)
            {
                bool isKoiIdValid = orderDetail.KoiId.HasValue && !orderDetail.BatchKoiId.HasValue;
                bool isBatchKoiIdValid = orderDetail.BatchKoiId.HasValue && !orderDetail.KoiId.HasValue;

                var orderDetail2 = await _koiShopV1DbContext.OrderDetails
                    .Where(od => (isKoiIdValid && orderdetailKoiId.Contains(orderDetail.KoiId.Value)) 
                    || (isBatchKoiIdValid && orderdetailBatchKoiId.Contains(orderDetail.BatchKoiId.Value)))
                    .FirstOrDefaultAsync();
                if (orderDetail2 != null)
                {
                    {
                        orderDetail2.Status = "AwaitingCareApproval";
                        _koiShopV1DbContext.OrderDetails.Update(orderDetail2);
                    }
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckRequest(int? id)
        {
            var request = await _koiShopV1DbContext.Requests.Where(r => r.RequestId == id
            && (r.Status == "UnderCare" || r.Status == "Pending")).FirstOrDefaultAsync();
            if (request == null)
                return false;
            return true;
        }
        public async Task<bool> UpdateKoiOrBatchToCare(int? id, string? status)
        {
            var user = _userContext.GetCurrentUser();
            if (user.Id == null || id == null || id <= 0)
            {
                return false;
            }
            var request = await _koiShopV1DbContext.Requests.FirstOrDefaultAsync(r => r.RequestId == id);
            if (request == null)
            {
                return false;
            }
            var reviewStatus = request.Status;
            if (!string.IsNullOrEmpty(status))
            {
                reviewStatus = status;
            }
            request.Status = reviewStatus;
            _koiShopV1DbContext.Requests.Update(request);
            var package1 = await _koiShopV1DbContext.Packages.Where(p => p.PackageId == request.PackageId).FirstOrDefaultAsync();
            var orderDetailToChange = await _koiShopV1DbContext.OrderDetails.Where(od => (od.KoiId == package1.KoiId && package1.BatchKoiId == null)
                || (od.BatchKoiId == package1.BatchKoiId && package1.KoiId == null)).FirstOrDefaultAsync();
            orderDetailToChange.Status = "UnderCare";
            _koiShopV1DbContext.OrderDetails.Update(orderDetailToChange);
            if (status == "RefusedCare" || status == "CompletedCare")
            {
                var package = await _koiShopV1DbContext.Packages.FirstOrDefaultAsync(p => p.PackageId == request.PackageId);
                if (package != null)
                {
                    var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.KoiId == package.KoiId
                    || od.BatchKoiId == package.BatchKoiId).FirstOrDefaultAsync();
                    if (orderDetail != null)
                    {
                        orderDetail.Status = "Pending";
                        _koiShopV1DbContext.OrderDetails.Update(orderDetail);
                    }
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
    }
}
