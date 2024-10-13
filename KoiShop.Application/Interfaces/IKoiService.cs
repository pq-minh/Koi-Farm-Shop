using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IKoiService
    {
        // Koi Methods =============================================================================================
        Task<IEnumerable<Koi>> GetAllKoi();

        Task<Koi> AddKoi(Koi koi);

        Task<bool> UpdateKoi(Koi koi);

        Task<bool> ValidateAddKoiDtoInfo(AddKoiDto koi);
        Task<bool> ValidateFishTypeIdInKoi(int FishTypeId);

        // KoiCategory Methods ======================================================================================
        Task<IEnumerable<KoiCategory>> GetAllKoiCategory();
        Task<KoiCategory> AddKoiCategory(KoiCategory kc);

        Task<KoiCategory> UpdateKoiCategory(KoiCategory kc);
        Task<Koi> GetKoiById(int id);
        Task<List<Koi>> GetKoiInKoiCategory(int fishTypeId);
        Task<bool> DeleteKoiCategory(int fishTypeId);
    }
}
