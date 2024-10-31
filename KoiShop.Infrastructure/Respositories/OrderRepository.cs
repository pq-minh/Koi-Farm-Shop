using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Migrations;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
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
        private readonly IDiscountRepository _discountRepository;
        public OrderRepository(KoiShopV1DbContext koiShopV1DbContext, IUserStore<User> userStore, IUserContext userContext, IDiscountRepository discountRepository)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userStore = userStore;
            _userContext = userContext;
            _discountRepository = discountRepository;
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
        public async Task<IEnumerable<Order>> GetOrder()
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orders = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<Order>();
            }
            return orders;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailById(int orderId)
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orders = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            var orderdetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == orderId).Include(od => od.Koi).
                Include(od => od.BatchKoi).ToListAsync();
            return orderdetail;
        }
        public async Task<IEnumerable<T>> GetKoiOrBatch<T>(int orderId)
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orders = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<T>();
            }
            var fish = await _koiShopV1DbContext.OrderDetails.
                Where(od => od.OrderId == orderId).ToListAsync();
            if (fish == null)
            {
                return Enumerable.Empty<T>();
            }
            var batch = await _koiShopV1DbContext.BatchKois.Where(b => fish.Any(f => b.BatchKoiId == f.BatchKoiId)).ToListAsync();
            var koi = await _koiShopV1DbContext.Kois.Where(k => fish.Any(f => k.KoiId == f.KoiId)).ToListAsync();
            if (typeof(T) == typeof(BatchKoi))
            {
                return batch as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(Koi))
            {
                return koi as IEnumerable<T>;
            }
            return Enumerable.Empty<T>();
        }
        public async Task<bool> AddToOrderDetailFromCart(List<CartItem> carts)
        {
            var user = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            var shoppingcart = await _koiShopV1DbContext.ShoppingCarts.Where(sc => sc.UserId == user.Id).FirstOrDefaultAsync();
            if (shoppingcart == null)
            {
                return false;
            }
            var exsitCart = await _koiShopV1DbContext.CartItems.Where(c => c.ShoppingCartId == shoppingcart.ShoppingCartID).
                ToListAsync();
            if (exsitCart == null)
            {
                return false;
            }
            foreach (var cart in carts)
            {
                if ((cart.KoiId.HasValue && !cart.BatchKoiId.HasValue) || (!cart.KoiId.HasValue && cart.BatchKoiId.HasValue))
                {
                    var orderDetail = new OrderDetail
                    {
                        KoiId = cart.KoiId,
                        BatchKoiId = cart.BatchKoiId,
                        ToTalQuantity = cart.Quantity,
                        OrderId = order.OrderId,
                        Price = cart.UnitPrice
                    };
                    _koiShopV1DbContext.OrderDetails.Add(orderDetail);
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddToOrderDetailFromShop(CartItem cart, int orderId)
        {
            var user = _userContext.GetCurrentUser();
            if ((cart.KoiId.HasValue && !cart.BatchKoiId.HasValue) || (!cart.KoiId.HasValue && cart.BatchKoiId.HasValue))
            {
                var orderDetail = new OrderDetail
                {
                    KoiId = cart.KoiId,
                    BatchKoiId = cart.BatchKoiId,
                    ToTalQuantity = cart.Quantity,
                    OrderId = orderId
                };
                _koiShopV1DbContext.Add(orderDetail);
                await _koiShopV1DbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public async Task<bool> AddToOrder(List<CartItem> carts, int? discountId, string? phoneNumber, string? address)
        {
            var user = _userContext.GetCurrentUser();
            var count = carts.Count();
            int quantity = 0;
            float totalPrice = 0;
            float totalAmount = 0;
            var order = new Order
            {
                TotalAmount = 0,
                CreateDate = DateTime.Now,
                OrderStatus = "Pending",
                UserId = user.Id,
                PhoneNumber = phoneNumber,
                ShippingAddress = address
            };
            _koiShopV1DbContext.Orders.Add(order);
            await _koiShopV1DbContext.SaveChangesAsync();
            foreach (var cart in carts)
            {
                var cartItem = await _koiShopV1DbContext.CartItems.FirstOrDefaultAsync(ci =>
                (cart.KoiId.HasValue && ci.KoiId == cart.KoiId) ||
                (cart.BatchKoiId.HasValue && ci.BatchKoiId == cart.BatchKoiId));
                if (cartItem != null)
                {
                    quantity = (int)cart.Quantity;
                    totalPrice = quantity * (float)cartItem.UnitPrice;
                    totalAmount += totalPrice;
                }
            }
            if (discountId.HasValue)
            {
                var pricePercentDiscount = await _discountRepository.CheckDiscount(discountId);
                if (pricePercentDiscount != null && pricePercentDiscount > 0 && pricePercentDiscount <= 100)
                {
                    totalAmount = totalAmount - (totalAmount * (float)(pricePercentDiscount / 100));
                    var discountUpdate = await _discountRepository.UpdateDiscountStatus((int)discountId);
                    order.DiscountId = discountId;
                }
            }
            order.TotalAmount = totalAmount;
            _koiShopV1DbContext.Orders.Update(order);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCartAfterBuy(List<CartItem> carts)
        {
            var user = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            var shoppingcart = await _koiShopV1DbContext.ShoppingCarts.Where(sc => sc.UserId == user.Id).FirstOrDefaultAsync();
            if (shoppingcart == null)
            {
                return false;
            }
            var existCart = await _koiShopV1DbContext.CartItems.Where(c => c.ShoppingCartId == shoppingcart.ShoppingCartID).
                ToListAsync();
            if (existCart == null)
            {
                return false;
            }
            var existOrderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).ToListAsync();
            if (existOrderDetail == null)
            {
                return false;
            }
            foreach (var cartItem in existCart)
            {
                var match = existOrderDetail.FirstOrDefault(eod =>
           (cartItem.KoiId.HasValue && eod.KoiId == cartItem.KoiId) ||
           (cartItem.BatchKoiId.HasValue && eod.BatchKoiId == cartItem.BatchKoiId));

                if (match != null)
                {
                    cartItem.Status = "Bought";
                    _koiShopV1DbContext.CartItems.Update(cartItem);
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPayment(string method)
        {
            var userId = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == userId.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }

            if (!string.Equals(method, "offline", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(method, "online", StringComparison.OrdinalIgnoreCase))
            {
                return false;

            }
            var payment = new Payment
            {
                CreateDate = DateTime.Now,
                PaymenMethod = method,
                Status = "Pending",
                OrderId = order.OrderId
            };
            _koiShopV1DbContext.Payments.Add(payment);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePayment()
        {
            var userId = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == userId.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            var payment = await _koiShopV1DbContext.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
            if (payment == null)
            {
                return false;
            }
            payment.Status = "Completed";
            _koiShopV1DbContext.Orders.Update(order);
            _koiShopV1DbContext.Payments.Update(payment);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> GetCurentOrderId()
        {
            var userId = _userContext.GetCurrentUser();
            var order = await _koiShopV1DbContext.Orders.Where(od => od.UserId == userId.Id).OrderByDescending(od => od.CreateDate).FirstOrDefaultAsync();
            int orderId = 0;
            if (order != null)
            {
                orderId = order.OrderId;
                if (orderId <= 0 || orderId == null)
                {
                    orderId = 0;
                }
            }
            var orderIdNext = orderId += 1;
            return orderIdNext;
        }
        public async Task<bool> UpdateKoiAndBatchStatus(List<CartItem> carts)
        {
            if (carts == null || carts.Count == 0)
            {
                return false;
            }

            foreach (var cart in carts)
            {
                if ((cart.KoiId.HasValue && (cart.BatchKoiId == null || !cart.BatchKoiId.HasValue)) ||
                    (cart.BatchKoiId.HasValue && (cart.KoiId == null || !cart.KoiId.HasValue)))
                {
                    if (cart.KoiId.HasValue)
                    {
                        var koi = await _koiShopV1DbContext.Kois.Where(k => k.KoiId == cart.KoiId).FirstOrDefaultAsync();
                        koi.Status = "Sold";
                        _koiShopV1DbContext.Kois.Update(koi);
                        await _koiShopV1DbContext.SaveChangesAsync();

                    }
                    else
                    {
                        var batchKoi = await _koiShopV1DbContext.BatchKois.Where(bk => bk.BatchKoiId == cart.BatchKoiId).FirstOrDefaultAsync();
                        batchKoi.Status = "Sold";
                        _koiShopV1DbContext.BatchKois.Update(batchKoi);
                        await _koiShopV1DbContext.SaveChangesAsync();

                    }
                }
                else
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByStaff()
        {
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.Status != "UnderCare").Include(o => o.Koi).Include(o => o.BatchKoi)
                .Include(o => o.Order.User).ToListAsync(); 
            return orderDetail;
        }

        public async Task<bool> UpdateOrderDetailsByStaff(int orderDetailId, string status)
        {
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => od.OrderDetailsId == orderDetailId).FirstOrDefaultAsync();
            if (orderDetail == null)
            {
                return false;
            }
            orderDetail.Status = status;
            _koiShopV1DbContext.OrderDetails.Update(orderDetail);
            var orderDetailGroup = await _koiShopV1DbContext.OrderDetails.GroupBy(od => od.OrderId).ToListAsync();
            if (orderDetailGroup != null)
            {
                foreach (var group in orderDetailGroup)
                {
                    bool allDeliverd = group.All(item => item.Status == "Delivered");
                    if (allDeliverd)
                    {
                        var order = await _koiShopV1DbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == group.Key);
                        if (order != null)
                        {
                            var payment = await _koiShopV1DbContext.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId
                            && p.Status == "Completed");
                            if (payment == null)
                            {
                                order.OrderStatus = "AwaitingPayment";  
                            }
                            else if(payment != null)
                            {
                                order.OrderStatus = "Completed";
                            }
                            _koiShopV1DbContext.OrderDetails.Update(orderDetail);
                        }
                    }
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }


        // =================================================================================================
        public async Task<IEnumerable<Order>> GetOrders(string status, DateTime startDate, DateTime endDate)
        {
            return await _koiShopV1DbContext.Orders
                .Where(o => o.OrderStatus == status &&
                            o.CreateDate.HasValue &&
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrders(DateTime startDate, DateTime endDate)
        {
            return await _koiShopV1DbContext.Orders
                .Where(o => o.CreateDate.HasValue &&
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(string status, DateTime startDate, DateTime endDate)
        {
            var orders = await _koiShopV1DbContext.Orders
                .Where(o => o.OrderStatus == status &&
                            o.CreateDate.HasValue &&
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .SelectMany(od => od.OrderDetails).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(DateTime startDate, DateTime endDate)
        {
            var orders = await _koiShopV1DbContext.Orders
                .Where(o => o.CreateDate.HasValue &&
                            o.CreateDate >= startDate &&
                            o.CreateDate <= endDate)
                .SelectMany(od => od.OrderDetails).ToListAsync();
            return orders;
        }


        public async Task<Order> GetOrderById(int orderId)
        {
            return await _koiShopV1DbContext.Orders.FindAsync(orderId);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _koiShopV1DbContext.Orders.Update(order);
            var result = await _koiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Payment> GetPaymentByOrderId(int orderId)
        {
            return await _koiShopV1DbContext.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment> GetPaymentById(int paymentId)
        {
            return await _koiShopV1DbContext.Payments.FindAsync(paymentId);

        }

        public async Task<bool> UpdatePayment(Payment payment)
        {
            _koiShopV1DbContext.Payments.Update(payment);
            var result = await _koiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsInOrder(int orderId)
        {
            return await _koiShopV1DbContext.OrderDetails.
                Where(o => o.OrderId == orderId).ToListAsync();
        }
    }
}
