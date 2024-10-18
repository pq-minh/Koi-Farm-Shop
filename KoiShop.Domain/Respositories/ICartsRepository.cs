using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface ICartsRepository
    {
        Task<IEnumerable<CartItem>> GetCart();
        Task<bool> AddItemToCart(CartItem cart);
        Task<bool> RemoveCart(CartItem cart);
        Task<bool> ChangeBatchQuantity(string? status, int batchKoiId);
    }
}
