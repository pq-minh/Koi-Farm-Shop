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
        Task<IEnumerable<BatchKoi>> GetAllBatchKoiAsync();
        Task<BatchKoi> AddBatchKoiAsync(BatchKoi batchKoi);
        Task<BatchKoi> UpdateBatchKoiAsync(BatchKoi batchKoi);
        Task<bool> DeleteBatchKoiAsync(int id);
        Task<BatchKoi> GetBatchKoiByIdAsync(int id);

    }

}
