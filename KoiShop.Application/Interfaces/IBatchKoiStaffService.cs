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
    public interface IBatchKoiStaffService
    {
        // BatchKoi Methods ===========================================================================================
        Task<IEnumerable<BatchKoi>> GetAllBatchKoiStaff();
        Task<BatchKoi> GetBatchKoiById(int id);
        Task<bool> AddBatchKoi(AddBatchKoiDto batchKoiDto);
        Task<bool> UpdateBatchKoi(UpdateBatchKoiDto batchKoiDto);
        Task<string> UpdateImage(IFormFile imageFile, string oldImagePath, string direction);
        Task<bool> UpdateBatchKoiStatus(int batchkoiId, string status);
        // BatchKoiCategory Methods ====================================================================================
        Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategory();
        Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategory(int batchTypeId);
    }
}
