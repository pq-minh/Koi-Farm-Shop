using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Infrastructure.Respositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly KoiShopV1DbContext _koiShopV1DbContext;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        public OrderRepository(KoiShopV1DbContext koiShopV1DbContext, IUserStore<User> userStore, IUserContext userContext)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userStore = userStore;
            _userContext = userContext;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetail()
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orders = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            var orderdetail = await _koiShopV1DbContext.OrderDetails.Where(od => orders.Select(o => o.OrderId).Contains((int)od.OrderId)).ToListAsync();
            return orderdetail;
        }

        public async Task<bool> AddToOrder(Order cart)
        {
            var user = _userContext.GetCurrentUser();
            cart.UserId = user.Id;
            if (cart.UserId == null || cart == null)
            {
                return false;
            }
            cart.CreateDate = DateTime.Now;
            cart.OrderStatus = "Pending";

            _koiShopV1DbContext.Orders.Add(cart);
            await _koiShopV1DbContext.SaveChangesAsync();
            foreach (var item in cart.OrderDetails)
            {
                item.OrderId = cart.OrderId;
                _koiShopV1DbContext.OrderDetails.Add(item);
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPayment(string method, string status)
        {
            var userId = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == userId.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            var payment = new Payment
            {
                CreateDate = DateTime.Now,
                PaymenMethod = method,
                Status = status,
                OrderId = order.OrderId
            };
            _koiShopV1DbContext.Payments.Add(payment);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
    }
}
