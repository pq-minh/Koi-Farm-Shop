using AutoMapper;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;
using KoiShop.Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using PhoneNumbers;
using static Google.Rpc.Context.AttributeContext.Types;
using KoiShop.Application.Dtos.VnPayDtos;
using System.Runtime.CompilerServices;
using KoiShop.Application.Dtos.OrderDtos;
using System.Numerics;

namespace KoiShop.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        private readonly IVnPayService _vnPayService;
        List<string> orderStatus = new() { "Pending", "Completed", "Shipped", "InTransit" };
        List<string> paymentStatus = new() { "Pending", "Completed" };

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IUserContext userContext, IUserStore<User> userStore, IVnPayService vpnPayService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userContext = userContext;
            _userStore = userStore;
            _vnPayService = vpnPayService;
        }
        public async Task<IEnumerable<OrderDetailDtos>> GetOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var od = await _orderRepository.GetOrderDetail();
            var oddto = _mapper.Map<IEnumerable<OrderDetailDtos>>(od);
            return oddto;
        }
        public async Task<IEnumerable<OrderDtos>> GetOrder()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDtos>();
            }
            var order = await _orderRepository.GetOrder();
            var orderDto = _mapper.Map<IEnumerable<OrderDtos>>(order);
            return orderDto;
        }
        public async Task<IEnumerable<OrderDetailDtoV2>> GetOrderDetailById(int? id)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || id == null)
            {
                return Enumerable.Empty<OrderDetailDtoV2>();
            }
            var orderDetail = await _orderRepository.GetOrderDetailById((int)id);
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDtoV2>>(orderDetail);
            return orderDetailDto;
        }
        public async Task<IEnumerable<KoiDto>> GetKoiFromOrderDetail(int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }
            var koi = await _orderRepository.GetKoiOrBatch<Koi>((int)orderId);
            var koiDto = _mapper.Map<IEnumerable<KoiDto>>(koi);
            return koiDto;
        }
        public async Task<IEnumerable<BatchKoiDto>> GetBatchFromOrderDetail(int? orderId)
        {
            if (orderId == null)
            {
                return null;
            }
            var batch = await _orderRepository.GetKoiOrBatch<BatchKoi>((int)orderId);
            var batchDto = _mapper.Map<IEnumerable<BatchKoiDto>>(batch);
            return batchDto;
        }

        public async Task<OrderEnum> AddOrders(List<CartDtoV2> carts, string method, int? discountId, string? phoneNumber, string? address, VnPaymentResponseFromFe request)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                return OrderEnum.NotLoggedInYet;
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return OrderEnum.UserNotAuthenticated;
            }
            foreach (var cart in carts)
            {
                if ((cart.KoiId == null && cart.BatchKoiId == null) || (cart.KoiId.HasValue && cart.KoiId <= 0) || (cart.BatchKoiId.HasValue && cart.BatchKoiId <= 0) ||
                    (cart.KoiId.HasValue && cart.BatchKoiId.HasValue))
                {
                    return OrderEnum.InvalidParameters;
                }
            }
            if (phoneNumber == null || address.IsNullOrEmpty() || !IsPhoneNumberValid(phoneNumber, "VN"))
            {
                return OrderEnum.InvalidTypeParameters;
            }
            var cartItems = _mapper.Map<List<CartItem>>(carts);
            var order = await _orderRepository.AddToOrder(cartItems, discountId, phoneNumber, address);
            if (order)
            {
                var orderDetail = await _orderRepository.AddToOrderDetailFromCart(cartItems);
                if (orderDetail)
                {
                    var cartStatus = await _orderRepository.UpdateCartAfterBuy(cartItems);
                    if (cartStatus)
                    {
                        var koiandBatchStatus = await _orderRepository.UpdateKoiAndBatchStatus(cartItems);
                        if (koiandBatchStatus)
                        {
                            var payment = await _orderRepository.AddPayment(method);
                            if (payment && method == "offline")
                            {
                                return OrderEnum.Success;
                            }
                            else if (method == "online")
                            {
                                var response = _vnPayService.ExecutePayment(request);
                                if (response == null || !response.Success)
                                {
                                    return OrderEnum.FailPaid;
                                }
                                var paymentUpdate = await _orderRepository.UpdatePayment();
                                if (paymentUpdate)
                                {
                                    return OrderEnum.Success;
                                }
                                else
                                {
                                    return OrderEnum.FailAddPayment;
                                }
                            }
                            else
                            {
                                return OrderEnum.FailAddPayment;
                            }
                        }
                        else
                        {
                            return OrderEnum.FailUpdateFish;
                        }
                    }
                    else
                    {
                        return OrderEnum.FailUpdateCart;
                    }
                }
                else
                {
                    return OrderEnum.FailAdd;
                }
            }
            else
                return OrderEnum.Fail;
        }

        public async Task<PaymentDto> PayByVnpay(VnPaymentResponseFromFe request)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                return new PaymentDto
                {
                    Status = OrderEnum.NotLoggedInYet,
                    Message = "User not logged in."
                };
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return new PaymentDto
                {
                    Status = OrderEnum.UserNotAuthenticated,
                    Message = "User not authenticated."
                };
            }
            if (request == null)
            {
                return new PaymentDto
                {
                    Status = OrderEnum.InvalidParameters,
                    Message = "Paramaters can not identify"
                };
            }
            var response = _vnPayService.ExecutePayment(request);
            if (response == null || !response.Success)
            {
                return new PaymentDto
                {
                    Status = OrderEnum.FailAddPayment,
                    Message = $"PaymentFail {response?.VnPayResponseCode}"
                };

            }
            var paymentUpdate = await _orderRepository.UpdatePayment();
            if (paymentUpdate)
            {
                return new PaymentDto
                {
                    Status = OrderEnum.Success,
                    Message = "Payment successful."
                };
            }
            else
            {
                return new PaymentDto
                {
                    Status = OrderEnum.Fail,
                    Message = "Payment unsuccessful."
                };
            }
        }
        private bool IsPhoneNumberValid(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, regionCode);
                return phoneNumberUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
        public async Task<IEnumerable<OrderDetailDtoV3>> GetOrderDetailsByStaff()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDetailDtoV3>();
            }
            var orderdetail = await _orderRepository.GetOrderDetailsByStaff();
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDtoV3>>(orderdetail);
            return orderDetailDto;
        }
        public async Task<bool> UpdateOrderDetailsByStaff(int? orderDetailId, string? status)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return false;
            }
            if (status == null || !orderDetailId.HasValue)
            {
                return false;
            }
            string[] statusArr = { "Delivered", "Shipped", "In Transit", "Pending", "Packing" };
            string? statusAccepted = null;
            foreach (var x in statusArr)
            {
                if (status == x)
                {
                    statusAccepted = x;
                }
                break;
            }
            if (string.IsNullOrEmpty(statusAccepted))
            {
                return false;
            }
            var result = await _orderRepository.UpdateOrderDetailsByStaff((int)orderDetailId, statusAccepted);
            if (result)
                return true;
            return false;
        }


        // ====================================================================================================
        public async Task<IEnumerable<Order>> GetOrders(string status, DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetOrders(status, startDate, endDate);
        }

        public async Task<int> GetBestSalesKoi(DateTime startDate, DateTime endDate)
        {
            Dictionary<int, int> koiDic = new Dictionary<int, int>();

            var od = await _orderRepository.GetOrderDetails("Completed", startDate, endDate);
            foreach (var item in od)
            {
                if (item.KoiId.HasValue)
                {
                    if (koiDic.ContainsKey(item.KoiId.Value))
                        koiDic[item.KoiId.Value] += (int)item.ToTalQuantity;
                    else
                        koiDic.Add((int)item.KoiId, (int)item.ToTalQuantity);
                }
            }

            int maxQuantity = 0;
            int id = -1;
            foreach (var item in koiDic)
            {
                if (item.Value > maxQuantity)
                {
                    maxQuantity = item.Value;
                    id = item.Key;
                }
            }
            return id;
        }
        public async Task<int> GetBestSalesBatchKoi(DateTime startDate, DateTime endDate)
        {
            Dictionary<int, int> batchKoiDic = new Dictionary<int, int>();

            var od = await _orderRepository.GetOrderDetails("Completed", startDate, endDate);
            foreach (var item in od)
            {
                if (item.KoiId.HasValue)
                {
                    if (batchKoiDic.ContainsKey(item.BatchKoiId.Value))
                        batchKoiDic[item.BatchKoiId.Value] += (int)item.ToTalQuantity;
                    else
                        batchKoiDic.Add((int)item.BatchKoiId, (int)item.ToTalQuantity);
                }
            }

            int maxQuantity = 0;
            int id = -1;
            foreach (var item in batchKoiDic)
            {
                if (item.Value > maxQuantity)
                {
                    maxQuantity = item.Value;
                    id = item.Key;
                }
            }
            return id;
        }



        public async Task<int> CountTotalOrders(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrders(startDate, endDate);
            int count = 0;
            foreach (var item in orders)
            {
                count++;
            }
            return count;
        }

        //public async Task<int> GetCompletedOrders(DateTime startDate, DateTime endDate)
        //{
        //    var orders = await _orderRepository.GetOrders("Complete", startDate, endDate);
        //    int count = 0;
        //    foreach (var item in orders)
        //    {
        //        count++;
        //    }
        //    return count;
        //}

        //public async Task<int> GetPendingOrders(DateTime startDate, DateTime endDate)
        //{
        //    var orders = await _orderRepository.GetOrders("Pending", startDate, endDate);
        //    int count = 0;
        //    foreach (var item in orders)
        //    {
        //        count++;
        //    }
        //    return count;
        //}

        public async Task<int> CountOrders(string status, DateTime startDate, DateTime endDate)
        {
            if (!orderStatus.Contains(status)) return -1;

            var orders = await _orderRepository.GetOrders(status, startDate, endDate);
            int count = 0;
            foreach (var item in orders)
            {
                count++;
            }
            return count;
        }


        public async Task<bool> UpdateOrder(UpdateOrderDtos order)
        {
            var currentOrder = await _orderRepository.GetOrderById(order.OrderId);
            if (currentOrder == null)
            {
                return false;
            }

            if (order.TotalAmount.HasValue) currentOrder.TotalAmount = order.TotalAmount.Value;
            if (order.CreateDate.HasValue) currentOrder.CreateDate = order.CreateDate.Value;
            if (!string.IsNullOrEmpty(order.OrderStatus)) currentOrder.OrderStatus = order.OrderStatus;
            if (order.DiscountId.HasValue) currentOrder.DiscountId = order.DiscountId.Value;
            if (!string.IsNullOrEmpty(order.PhoneNumber)) currentOrder.PhoneNumber = order.PhoneNumber;
            if (!string.IsNullOrEmpty(order.ShippingAddress)) currentOrder.ShippingAddress = order.ShippingAddress;

            return await _orderRepository.UpdateOrder(currentOrder);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            if (!orderStatus.Contains(status)) return false;

            var payment = await _orderRepository.GetPaymentByOrderId(orderId);
            if (payment == null) return false;

            // payment complete thì order ms dc complete
            if (status == "Completed")
                if (payment.Status != "Completed")
                    return false;

            var orders = await _orderRepository.GetOrderById(orderId);
            if (orders == null) return false;

            orders.OrderStatus = status;

            return await _orderRepository.UpdateOrder(orders);
        }


        public async Task<bool> UpdatePaymentStatus(int paymentId, string status)
        {
            if (!paymentStatus.Contains(status)) return false;

            var payment = await _orderRepository.GetPaymentById(paymentId);
            if (payment == null) return false;

            payment.Status = status;

            return await _orderRepository.UpdatePayment(payment);
        }


        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(string status, DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetOrderDetails(status, startDate, endDate);
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsInOrder(int orderId)
        {
            return await _orderRepository.GetOrderDetailsInOrder(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }


        public async Task<IEnumerable<Order>> GetOrdersByStatus(string status)
        {
            if (!orderStatus.Contains(status)) return null;
            return await _orderRepository.GetOrdersByStatus(status);
        }
    }
}
