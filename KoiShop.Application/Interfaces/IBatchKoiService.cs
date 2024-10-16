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
    public interface IBatchKoiService
    {
        Task<IEnumerable<BatchKoiDto>> GetAllBatchKoi();
        Task<BatchKoiDto> GetBatchKoi(int id);
        Task<IEnumerable<BatchKoiDto>> GetAllBatchKoiWithCondition(KoiFilterDto koiFilterDto);

        // Staff ================
        // BatchKoi Methods ===========================================================================================
        Task<IEnumerable<BatchKoi>> GetAllBatchKoiStaff();
        Task<BatchKoi> GetBatchKoiById(int id);
        Task<bool> AddBatchKoi(AddBatchKoiDto batchKoiDto, string imageUrl);
        Task<bool> UpdateBatchKoi(BatchKoi batchKoi);
        Task<bool> ValidateAddBatchKoiDtoInfo(AddBatchKoiDto batchKoi);
        Task<bool> ValidateBatchTypeIdInBatchKoi(int batchTypeId);
        Task<BatchKoi> ValidateUpdateBatchKoiDto(int batchKoiId, UpdateBatchKoiDto batchKoiDto);
        // BatchKoiCategory Methods ====================================================================================
        Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategory();
        Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategory(int batchTypeId);
    }
}