﻿using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Infrastructure.Respositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        private readonly KoiShopV1DbContext _koiShopV1DbContext;
        public CartsRepository(KoiShopV1DbContext koiShopV1DbContext, IUserContext userContext, IUserStore<User> userStore)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userContext = userContext;
            _userStore = userStore;
        }

        public async Task<IEnumerable<CartItem>> GetCart()
        {
            var userId = _userContext.GetCurrentUser();
            var shoppingCart = await _koiShopV1DbContext.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId.Id);
            if (shoppingCart == null)
            {
                return Enumerable.Empty<CartItem>();
            }
            var cartItems = await _koiShopV1DbContext.CartItems.
                Where(ci => (ci.ShoppingCartId == shoppingCart.ShoppingCartID) && (ci.Status == "Save")).ToListAsync();
            foreach (var cartItem in cartItems)
            {
                double currentPrice = await GetCurrentPrice(cartItem);

                if (cartItem.UnitPrice != (float)currentPrice)
                {
                    cartItem.UnitPrice = (float)currentPrice;
                    cartItem.TotalPrice = cartItem.Quantity * cartItem.UnitPrice;
                    _koiShopV1DbContext.CartItems.Update(cartItem);
                }
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return cartItems;
        }
        private async Task<double> GetCurrentPrice(CartItem cart)
        {
            double currentPrice = 0;
            if (cart != null)
            {
                if (cart.KoiId.HasValue)
                {
                    currentPrice = (double)await _koiShopV1DbContext.Kois
                        .Where(k => k.KoiId == cart.KoiId)
                        .Select(k => k.Price)
                        .FirstOrDefaultAsync();
                    return currentPrice;
                }
                else if (cart.BatchKoiId.HasValue)
                {
                    currentPrice = (double)await _koiShopV1DbContext.BatchKois
                        .Where(b => b.BatchKoiId == cart.BatchKoiId)
                        .Select(b => b.Price)
                        .FirstOrDefaultAsync();
                    return currentPrice;
                }
            }
            return currentPrice;
        }
        public async Task<bool> AddItemToCart(CartItem cart)
        {
            var userId = _userContext.GetCurrentUser().Id;
            var shoppingCart = await _koiShopV1DbContext.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId);
            #region AddNewShoppingcart
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { UserId = userId, CreateDate = DateTime.Now };
                _koiShopV1DbContext.ShoppingCarts.Add(shoppingCart);
                await _koiShopV1DbContext.SaveChangesAsync();
            }
            #endregion
            var exsitFish = _koiShopV1DbContext.CartItems.FirstOrDefault(c => ((cart.KoiId.HasValue && cart.KoiId == c.KoiId) || (cart.BatchKoiId.HasValue && cart.BatchKoiId == c.BatchKoiId)) &&
              (c.ShoppingCartId == shoppingCart.ShoppingCartID));
            if (exsitFish != null)
            {
                var currentPrice = await GetCurrentPrice(cart);
                if (exsitFish.UnitPrice != (float)currentPrice)
                {
                    exsitFish.UnitPrice = (float)currentPrice;
                    exsitFish.TotalPrice = (float)currentPrice * exsitFish.Quantity;
                }
                if (cart.BatchKoiId.HasValue && cart.BatchKoiId == exsitFish.BatchKoiId)
                {
                    exsitFish.UnitPrice = (float)currentPrice;
                    exsitFish.TotalPrice = (float)currentPrice * exsitFish.Quantity;
                    _koiShopV1DbContext.CartItems.Update(exsitFish);
                    await _koiShopV1DbContext.SaveChangesAsync();
                }
                exsitFish.Status = "Save";
                _koiShopV1DbContext.CartItems.Update(exsitFish);
                await _koiShopV1DbContext.SaveChangesAsync();
            }
            else
            {
                #region Checkprice
                double price = 0;
                if (cart.KoiId.HasValue)
                {
                    price = (double)await _koiShopV1DbContext.Kois.Where(k => k.KoiId == cart.KoiId).Select(k => k.Price).
                       FirstOrDefaultAsync();
                }
                else if (cart.BatchKoiId.HasValue)
                {
                    price = (double)await _koiShopV1DbContext.BatchKois.Where(b => b.BatchKoiId == cart.BatchKoiId).Select(b => b.Price).
                            FirstOrDefaultAsync();
                }
                #endregion
                var cartItems = new CartItem
                {
                    KoiId = cart.KoiId,
                    BatchKoiId = cart.BatchKoiId,
                    Quantity = 1,
                    UnitPrice = (float)price,
                    TotalPrice = (float)(price * 1),
                    Status = "Save",
                    ShoppingCartId = shoppingCart.ShoppingCartID
                };
                _koiShopV1DbContext.CartItems.Add(cartItems);
                await _koiShopV1DbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> RemoveCart(CartItem cart)
        {
            var userId = _userContext.GetCurrentUser().Id;
            var shoppingCart = await _koiShopV1DbContext.ShoppingCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();
            var exsitFish = _koiShopV1DbContext.CartItems.FirstOrDefault(c => 
            ((cart.KoiId.HasValue && cart.KoiId == c.KoiId) || 
            (cart.BatchKoiId.HasValue && cart.BatchKoiId == c.BatchKoiId)) &&
              (c.ShoppingCartId == shoppingCart.ShoppingCartID));
            if (exsitFish != null)
            {
                exsitFish.Status = "Removed";
                exsitFish.Quantity = 1;
                var remove = _koiShopV1DbContext.CartItems.Update(exsitFish);
                await _koiShopV1DbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<Dictionary<int, (string Name, string Description, string ImgUrl)>> GetKoiNamesAsync(IEnumerable<int> koiIds)
        {
            return await _koiShopV1DbContext.Kois
                .Where(kn => koiIds.Contains(kn.KoiId))
                .ToDictionaryAsync(kn => kn.KoiId, kn => (kn.Name, kn.Description, kn.Image));
        }

        public async Task<Dictionary<int, (string Name, string Description, string ImgUrl)>> GetBatchKoiNamesAsync(IEnumerable<int> batchKoiIds)
        {
            return await _koiShopV1DbContext.BatchKois
                .Where(bk => batchKoiIds.Contains(bk.BatchKoiId))
                .ToDictionaryAsync(kn => kn.BatchKoiId, kn => (kn.Name, kn.Description, kn.Image));
        }
        public async Task<bool> ChangeBatchQuantity(string? status, int batchKoiId)
        {
            var userId = _userContext.GetCurrentUser().Id;

            if (string.IsNullOrEmpty(status))
            {
                return false;
            }

            var cart = await _koiShopV1DbContext.CartItems
                .Where(c => c.BatchKoiId == batchKoiId && c.Status == "Save")
                .FirstOrDefaultAsync();

            if (cart != null)
            {
                if (status.Equals("Add", StringComparison.OrdinalIgnoreCase))
                {
                    cart.Quantity += 1;
                    
                }
                else if (status.Equals("Minus", StringComparison.OrdinalIgnoreCase))
                {
                    if (cart.Quantity > 1)
                    {
                        cart.Quantity -= 1;
                    }
                    else
                    {
                        return false;
                    }
                }
                cart.TotalPrice = cart.Quantity*cart.UnitPrice;
                _koiShopV1DbContext.CartItems.Update(cart);
                var changes = await _koiShopV1DbContext.SaveChangesAsync();
                return changes > 0; 
            }

            return false;
        }

    }
}
