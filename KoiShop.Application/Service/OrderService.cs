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
using KoiShop.Domain.Respositories;
using KoiShop.Application.Dtos;

namespace KoiShop.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userContext = userContext;
            _userStore = userStore;
        }
        public async Task<IEnumerable<OrderDto>> GetOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDto>();
            }
            var od = await _orderRepository.GetOrderDetail();
            var oddto = _mapper.Map<IEnumerable<OrderDto>>(od);
            return oddto;
        }
        public async Task<OrderEnum> AddOrders(List<CartDtoV2> carts, string method)
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
            var cartItems = _mapper.Map<List<CartItem>>(carts);
            var order = await _orderRepository.AddToOrder(cartItems);
            if (order)
            {
                var orderDetail = await _orderRepository.AddToOrderDetailFromCart(cartItems);
                if (orderDetail)
                {
                    var cartstatus = await _orderRepository.UpdateCartAfterBuy(cartItems);
                    if (cartstatus)
                    {
                        var payment = await _orderRepository.AddPayment(method);
                        if (payment)
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
    }
}
