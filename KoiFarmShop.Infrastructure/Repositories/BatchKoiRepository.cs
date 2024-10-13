using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Domain.Interfaces;
using KoiFarmShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Repositories
{
    public class BatchKoiRepository : IBatchKoiRepository
    {
        private readonly KoiFarmShopContext _context;

        public BatchKoiRepository(KoiFarmShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoiAsync()
        {
            return await _context.BatchKois.ToListAsync();
        }
        public async Task<BatchKoi> AddBatchKoiAsync(BatchKoi batchKoi)
        {
            _context.BatchKois.Add(batchKoi);
            await _context.SaveChangesAsync();
            return batchKoi;
        }

        public async Task<BatchKoi> UpdateBatchKoiAsync(BatchKoi batchKoi)
        {
            _context.BatchKois.Update(batchKoi);
            await _context.SaveChangesAsync();
            return batchKoi;
        }

        public async Task<bool> DeleteBatchKoiAsync(int id)
        {
            var batchKoi = await _context.BatchKois.FindAsync(id);
            if (batchKoi == null)
            {
                return false;
            }

            _context.BatchKois.Remove(batchKoi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BatchKoi> GetBatchKoiByIdAsync(int id)
        {
            return await _context.BatchKois.FindAsync(id);
        }
    }

}
