using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IKoiStaffService
    {
        // Koi Methods =============================================================================================
        Task<IEnumerable<Koi>> GetAllKoiStaff();
        Task<Koi> GetKoiById(int id);
        Task<bool> AddFish(AddKoiDto koiDto);
        Task<string> UpdateImage(IFormFile imageFile, string oldImagePath, string directory);
        Task<bool> UpdateFish(UpdateKoiDto koiDto);
        Task<bool> UpdateFishStatus(int koiId, string status);

        // FishCategory Methods ======================================================================================
        Task<IEnumerable<FishCategory>> GetAllFishCategory();
        Task<IEnumerable<Koi>> GetKoisInFishCategory(int fishTypeId);

    }
}
