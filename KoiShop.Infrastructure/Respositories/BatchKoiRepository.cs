using KoiShop.Domain.Entities;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using KoiShop.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Repositories
{
    public class BatchKoiRepository : IBatchKoiRepository
    {
        private readonly KoiShopV1DbContext _context;
        public BatchKoiRepository(KoiShopV1DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BatchKoi>> GetAllBatch()
        {
            return await _context.BatchKois.Include(bk => bk.BatchType).ToListAsync();
        }
        public async Task<BatchKoi> GetBatchKoi(int id)
        {
            return await _context.BatchKois.Where(b => b.BatchKoiId == id).Include(k => k.BatchType).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BatchKoi>> GetBatchKoiWithCondition(string batchKoiName, string typeBatch, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            var allBatchKoi = _context.BatchKois.Include(bk => bk.BatchType).AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(batchKoiName))
            {
                allBatchKoi = allBatchKoi.Where(bk => bk.Name.ToLower().Contains(batchKoiName.ToLower()));
            }
            if (!string.IsNullOrEmpty(typeBatch))
            {
                allBatchKoi = allBatchKoi.Where(bk => bk.BatchType.TypeBatch.ToLower().Contains(typeBatch.ToLower()));
            }
            if (from.HasValue)
            {
                allBatchKoi = allBatchKoi.Where(bk => bk.Price >= from);
            }
            if (to.HasValue)
            {
                allBatchKoi = allBatchKoi.Where(bk => bk.Price <= to);
            }
            #endregion

            #region Sort
            allBatchKoi = allBatchKoi.OrderBy(bk => bk.Name);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": allBatchKoi.OrderByDescending(bk => bk.Name); break;
                    case "price_asc": allBatchKoi.OrderBy(bk => bk.Price); break;
                    case "price_desc": allBatchKoi.OrderByDescending(bk => bk.Price); break;
                    default: allBatchKoi.OrderBy(bk => bk.Name); break;
                }
            }
            #endregion

            #region Paging
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            allBatchKoi = allBatchKoi.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            #endregion

            return await allBatchKoi.ToListAsync();
        }
    }
}
