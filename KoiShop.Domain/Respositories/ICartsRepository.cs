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

        Task<Dictionary<int, (string Name, string Description, string ImgUrl)>> GetKoiNamesAsync(IEnumerable<int> koiIds);

        Task<Dictionary<int, (string Name, string Description, string ImgUrl)>> GetBatchKoiNamesAsync(IEnumerable<int> batchKoiIds);
    }
}
