using KoiShop.Domain.Entities;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiShop.Domain.Respositories;

namespace KoiShop.Infrastructure.Respositories
{
    public class BatchKoiRepository : IBatchKoiRepository
    {
        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public BatchKoiRepository(KoiShopV1DbContext KoiShopV1DbContext)
        {
            _KoiShopV1DbContext = KoiShopV1DbContext;
        }

        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoiAsync()
        {
            return await _KoiShopV1DbContext.BatchKois.ToListAsync();
        }
        public async Task<BatchKoi> AddBatchKoiAsync(BatchKoi batchKoi)
        {
            _KoiShopV1DbContext.BatchKois.Add(batchKoi);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return batchKoi;
        }

        public async Task<BatchKoi> UpdateBatchKoiAsync(BatchKoi batchKoi)
        {
            _KoiShopV1DbContext.BatchKois.Update(batchKoi);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return batchKoi;
        }

        public async Task<bool> DeleteBatchKoiAsync(int id)
        {
            var batchKoi = await _KoiShopV1DbContext.BatchKois.FindAsync(id);
            if (batchKoi == null)
            {
                return false;
            }

            _KoiShopV1DbContext.BatchKois.Remove(batchKoi);
            await _KoiShopV1DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BatchKoi> GetBatchKoiByIdAsync(int id)
        {
            return await _KoiShopV1DbContext.BatchKois.FindAsync(id);
        }
    }

}
