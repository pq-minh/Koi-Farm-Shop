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

        // Koi Methods ============================================================================================
        Task<IEnumerable<Koi>> GetKois();
        Task<bool> AddKoi(Koi koi);
        Task<Koi> GetKoiById(int id);
        Task<bool> UpdateKoi(Koi koi);

        // KoiCategory Methods  =====================================================================================
        Task<IEnumerable<KoiCategory>> GetAllKoiCategories();
        Task<KoiCategory> GetKoiCategoryById(int id);
        Task<List<Koi>> GetKoisInKoiCategory(int fishTypeId);
    }
}
