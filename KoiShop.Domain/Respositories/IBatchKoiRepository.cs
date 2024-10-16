using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IBatchKoiRepository
    {
        Task<IEnumerable<BatchKoi>> GetAllBatch();
        Task<BatchKoi> GetBatchKoi(int id);
        Task<IEnumerable<BatchKoi>> GetBatchKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize);

        // Staff =================================================================================

        // BatchKoi Method ============================================================================================
        Task<IEnumerable<BatchKoi>> GetAllBatchKoiAsync();
        Task<bool> AddBatchKoiAsync(BatchKoi batchKoi);
        Task<bool> UpdateBatchKoiAsync(BatchKoi batchKoi);
        Task<BatchKoi> GetBatchKoiByIdAsync(int id);
        // BatchKoiCategory Method =====================================================================================
        Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategoryAsync();
        Task<BatchKoiCategory> GetBatchKoiCategoryByIdAsync(int id);
        Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategoryAsync(int fishTypeId);

    }
}
