﻿using KoiShop.Application.Dtos;
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
        Task<IEnumerable<KoiDto>> GetAllKoi();
        Task<KoiDto> GetKoi(int id);
        Task<IEnumerable<KoiDto>> GetAllKoiWithCondition(KoiFilterDto koiFilterDto);


        // Staff ====================

        // Koi Methods =============================================================================================
        Task<IEnumerable<Koi>> GetAllKoiStaff();
        Task<Koi> GetKoiById(int id);
        Task<bool> AddKoi(AddKoiDto koiDto, string imageUrl);

        Task<bool> UpdateKoi(Koi koi);
        Task<bool> ValidateAddKoiDtoInfo(AddKoiDto koiDto);
        Task<bool> ValidateFishTypeIdInKoi(int FishTypeId);
        Task<Koi> ValidateUpdateKoiDto(int koiId, UpdateKoiDto koiDto);

        // KoiCategory Methods ======================================================================================
        Task<IEnumerable<KoiCategory>> GetAllKoiCategory();
        Task<List<Koi>> GetKoiInKoiCategory(int fishTypeId);


    }
}