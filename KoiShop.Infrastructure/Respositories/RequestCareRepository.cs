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
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId && o.OrderStatus != "Delivered").Select(o => o.OrderId).ToListAsync();
            if (order == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            if (request == null || !request.Any())
            {
                var orderDetails = await _koiShopV1DbContext.OrderDetails.Where(od => order.Contains((int)od.OrderId))
                    .Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
                if (orderDetails == null)
                {
                    return Enumerable.Empty<OrderDetail>();
                }
                return orderDetails;
            }
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => order.Contains((int)od.OrderId) && !request.Any(r => r.Package.KoiId == od.KoiId || r.Package.BatchKoiId == od.BatchKoiId)).
                Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
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
            string[] status = { "Care", "Deliver", "Delivered" };
            var request = await _koiShopV1DbContext.Requests.Where(r => status.Any(s => r.Status.Contains(s))).Include(r => r.Package).Include(r => r.Package.Koi).Include(r => r.Package.BatchKoi).ToListAsync();
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
                    Status = "accepted"
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
                    Status = "accepted"
                };
                _koiShopV1DbContext.Requests.Add(request);
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateKoiOrBatchToCare(int? id)
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
            request.Status = "Delivery";
            _koiShopV1DbContext.Update(request);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
    }
}
