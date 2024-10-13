using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class KoiService : IKoiService
    {
        private readonly IKoiRepository _koiRepository;

        public KoiService(IKoiRepository koiRepository)
        {
            _koiRepository = koiRepository;
        }


        // Koi Methods =============================================================================================
        public async Task<IEnumerable<Koi>> GetAllKoi()
        {
            return await _koiRepository.GetAllKoiAsync();
        }

        public async Task<Koi> GetKoiById(int koiId)
        {
            return await _koiRepository.GetKoiByIdAsync(koiId);
        }

        public async Task<Koi> AddKoi(Koi koi)
        {
            return await _koiRepository.AddKoiAsync(koi);
        }

        public async Task<bool> UpdateKoi(Koi koi)
        {
            var result = await _koiRepository.UpdateKoiAsync(koi);
            return result != null;
        }

        public async Task<bool> ValidateAddKoiDtoInfo(AddKoiDto koi)
        {
            if (koi == null) return false;

            if (!await ValidateFishTypeIdInKoi(koi.FishTypeId)) return false;

            if (string.IsNullOrEmpty(koi.Name)) return false;
            if (string.IsNullOrEmpty(koi.Origin)) return false;
            if (string.IsNullOrEmpty(koi.Description)) return false;
            if (string.IsNullOrEmpty(koi.Gender)) return false;
            if (koi.ImageFile == null) return false;
            if (koi.Age <= 0) return false;
            if (koi.Weight <= 0) return false;
            if (koi.Size <= 0) return false;
            if (string.IsNullOrEmpty(koi.Personality)) return false;
            if (string.IsNullOrEmpty(koi.Status)) return false;
            if (koi.Price <= 0) return false;
            if (string.IsNullOrEmpty(koi.Certificate)) return false;

            return true;
        }


        public async Task<bool> ValidateFishTypeIdInKoi(int FishTypeId)
        {
            if (FishTypeId < 1) return false;
            // ktra xem có tồn tại FishTypeId trong Koi
            var KgList = await GetAllKoiCategory();
            bool contain = false;
            foreach (var item in KgList)
            {
                if (FishTypeId == item.FishTypeId)
                {
                    contain = true;
                }
            }
            if (!contain) return false;

            return true;
        }


        // KoiCategory Methods ======================================================================================
        public async Task<IEnumerable<KoiCategory>> GetAllKoiCategory()
        {
            return await _koiRepository.GetAllKoiCategoryAsync();
        }
        public async Task<KoiCategory> AddKoiCategory(KoiCategory kc)
        {
            return await _koiRepository.AddKoiCategoryAsync(kc);
        }

        public async Task<KoiCategory> UpdateKoiCategory(KoiCategory kc)
        {
            return await _koiRepository.UpdateKoiCategoryAsync(kc);
        }

        public async Task<List<Koi>> GetKoiInKoiCategory(int fishTypeId)
        {
            return await _koiRepository.GetKoiInKoiCategoryAsync(fishTypeId);
        }

        public async Task<bool> DeleteKoiCategory(int fishTypeId)
        {
            return await _koiRepository.DeleteKoiCategoryAsync(fishTypeId);
        }


    }

}
