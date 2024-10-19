using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetAllDiscounts();  
        Task<Discount> GetDiscountById(int id);
        Task<bool> AddDiscount(Discount discount);       
        Task<bool> UpdateDiscount(Discount discount);                
    }

}
