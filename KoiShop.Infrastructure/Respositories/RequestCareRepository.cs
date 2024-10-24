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

        public async Task<IEnumerable<Request>> GetKoiOrBatchCare()
        {
            var user = _userContext.GetCurrentUser();
            if (user == null)
            {
                return Enumerable.Empty<Request>();
            }
            var koiOrBatch = await _koiShopV1DbContext.Requests.Where(r => r.Status == "Care").Include(r => r.Package).Include(r => r.Package.Koi).Include(r => r.Package.BatchKoi).ToListAsync();
            if (koiOrBatch == null)
            {
                return Enumerable.Empty<Request>();
            }
            return koiOrBatch;
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
            var orderdetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).Include(od => od.Koi).
                Include(od => od.BatchKoi).ToListAsync();
            if (orderdetail == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            return orderdetail;
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
