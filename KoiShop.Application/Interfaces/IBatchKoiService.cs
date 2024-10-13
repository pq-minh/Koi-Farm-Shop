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
        Task<IEnumerable<BatchKoi>> GetAllBatchKoi();
        //Task<BatchKoi> GetBatchKoi(int id);
        //Task<BatchKoi> AddBatchKoi(BatchKoi batchKoi);
        //Task<bool> UpdateBatchKoi(BatchKoi batchKoi);

        //Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategories();
        //Task<BatchKoiCategory> GetBatchKoiCategory(int id);
        //Task<BatchKoiCategory> AddBatchKoiCategory(BatchKoiCategory batchKoiCategory);
        //Task<bool> UpdateBatchKoiCategory(BatchKoiCategory batchKoiCategory);
    }
}
