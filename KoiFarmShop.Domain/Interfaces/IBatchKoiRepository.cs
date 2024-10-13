using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Interfaces
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
