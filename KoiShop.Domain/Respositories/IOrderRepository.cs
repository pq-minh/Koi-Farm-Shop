using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetail();
        Task<bool> AddToOrder(List<CartItem> carts);
        Task<bool> AddPayment(string method);
        Task<bool> AddToOrderDetailFromShop(CartItem cart, int orderId);
        Task<bool> AddToOrderDetailFromCart(List<CartItem> carts);
        Task<bool> UpdateCartAfterBuy(List<CartItem> carts);
    }
}
