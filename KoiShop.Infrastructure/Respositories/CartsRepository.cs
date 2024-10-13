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
            var cartItem = await _koiShopV1DbContext.CartItems.
                Where(ci => (ci.ShoppingCartId == shoppingCart.ShoppingCartID) && (ci.Status == "Save")).ToListAsync();
            return cartItem;
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
                if (cart.BatchKoiId.HasValue && cart.BatchKoiId == exsitFish.BatchKoiId)
                {
                    exsitFish.Quantity += cart.Quantity;
                    exsitFish.UnitPrice += cart.UnitPrice;
                    exsitFish.TotalPrice = exsitFish.Quantity * exsitFish.UnitPrice;
                    _koiShopV1DbContext.Update(exsitFish);
                    await _koiShopV1DbContext.SaveChangesAsync();
                }
            }
            else
            {
                #region Checkprice
                double price = 0;
                if (cart.KoiId.HasValue) {
                     price = (double) await _koiShopV1DbContext.Kois.Where(k => k.KoiId == cart.KoiId).Select(k => k.Price).
                        FirstOrDefaultAsync();
                }
                if (cart.BatchKoiId.HasValue) {
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
                    TotalPrice = (float)price * cart.Quantity,
                    Status = "Save",
                    ShoppingCartId = shoppingCart.ShoppingCartID
                };
                _koiShopV1DbContext.CartItems.Add(cartItems);
                await _koiShopV1DbContext.SaveChangesAsync();
            }
            return true;

        }

    }
}
