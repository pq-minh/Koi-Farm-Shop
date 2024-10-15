using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
   public interface IKoiRepository
    {
        Task<IEnumerable<Koi>> GetAllKoi();
        Task<IEnumerable<Koi>> GetAllKois();
        Task<Koi> GetKoi(int id);
        Task<IEnumerable<Koi>> GetKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize);
    }
}
