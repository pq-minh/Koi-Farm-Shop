using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Users;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Service
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartsRepository _cartsRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;

        public CartService(IMapper mapper, ICartsRepository cartsRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _cartsRepository = cartsRepository;
            _userContext = userContext;
            _userStore = userStore;
        }
        public async Task<IEnumerable<CartDtos>> GetCart()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }

            var allCart = await _cartsRepository.GetCart();


            var koiIds = allCart
            .Select(ci => ci.KoiId)
            .Where(koiId => koiId.HasValue) 
            .Select(koiId => koiId.Value)   
            .Distinct()
            .ToList();

            var batchKoiIds = allCart
                .Select(ci => ci.BatchKoiId)
                .Where(batchKoiId => batchKoiId.HasValue) 
                .Select(batchKoiId => batchKoiId.Value)   
                .Distinct()
                .ToList();



            var koiNames = await _cartsRepository.GetKoiNamesAsync(koiIds);
            var batchKoiNames = await _cartsRepository.GetBatchKoiNamesAsync(batchKoiIds);

            var cartDtos = allCart.Select(cartItem => new CartDtos
            {
                CartItemsID = cartItem.CartItemsID,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice,
                TotalPrice = cartItem.TotalPrice,
                Status = cartItem.Status,
                KoiId = cartItem.KoiId, 
                BatchKoiId = cartItem.BatchKoiId,
                ShoppingCartId = cartItem.ShoppingCartId,
                KoiName = koiNames.GetValueOrDefault(cartItem.KoiId ?? 0).Name,
                BatchKoiName = batchKoiNames.GetValueOrDefault(cartItem.BatchKoiId ?? 0).Name,
                KoiImgUrl = koiNames.GetValueOrDefault(cartItem.KoiId ?? 0).ImgUrl,
                BatchKoiImgUrl = batchKoiNames.GetValueOrDefault(cartItem.BatchKoiId ?? 0).ImgUrl,
                KoiDescription = koiNames.GetValueOrDefault(cartItem.KoiId ?? 0).Description,
                BatchKoiDescription = batchKoiNames.GetValueOrDefault(cartItem.BatchKoiId ?? 0).Description
            });

            return cartDtos;
        }
        public async Task<CartEnum> AddCarts(CartDtoV1 cart)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                return CartEnum.NotLoggedInYet;
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return CartEnum.UserNotAuthenticated;
            }
            if (cart.KoiId == null && cart.BatchKoiId == null)
            {
                return CartEnum.InvalidParameters;
            }
            var cartItem = _mapper.Map<CartItem>(cart);
            var add = await _cartsRepository.AddItemToCart(cartItem);
            if (add)
                return CartEnum.Success;
            else
                return CartEnum.Fail;
        }
        public async Task<CartEnum> RemoveCart(CartDtoV1 cart)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                return CartEnum.NotLoggedInYet;
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return CartEnum.UserNotAuthenticated;
            }
            if (cart.KoiId == null && cart.BatchKoiId == null)
            {
                return CartEnum.InvalidParameters;
            }
            var cartItem = _mapper.Map<CartItem>(cart);
            var add = await _cartsRepository.RemoveCart(cartItem);
            if (add)
                return CartEnum.Success;
            else
                return CartEnum.Fail;
        }

    }
}
