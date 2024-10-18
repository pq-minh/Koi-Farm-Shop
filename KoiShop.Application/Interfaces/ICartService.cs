using KoiShop.Application.Dtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDtos>> GetCart();
        Task<CartEnum> AddCarts(CartDtoV1 cart);
        Task<CartEnum> RemoveCart(CartDtoV1 cart);
        Task<bool> ChangeBatchQuantity(string? status, int? batchKoiId);
    }
}
