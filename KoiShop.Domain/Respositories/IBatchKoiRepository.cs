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
    }
}
