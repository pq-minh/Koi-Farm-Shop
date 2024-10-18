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

        // Staff =================================================================================

        // Koi Methods ============================================================================================
        Task<IEnumerable<Koi>> GetAllKoiAsync();
        Task<bool> AddKoiAsync(Koi koi);
        Task<Koi> GetKoiByIdAsync(int id);
        Task<bool> UpdateKoiAsync(Koi koi);

        // KoiCategory Methods  =====================================================================================
        Task<IEnumerable<KoiCategory>> GetAllKoiCategoryAsync();
        Task<KoiCategory> GetKoiCategoryByIdAsync(int id);
        Task<List<Koi>> GetKoiInKoiCategoryAsync(int fishTypeId);
    }
}
