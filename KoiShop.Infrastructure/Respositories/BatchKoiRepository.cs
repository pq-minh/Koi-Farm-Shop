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
        private readonly KoiShopV1DbContext _KoiShopV1DbContext;
        public BatchKoiRepository(KoiShopV1DbContext context)
        {
            _KoiShopV1DbContext = context;
        }
        public async Task<IEnumerable<BatchKoi>> GetAllBatch()
        {
            return await _KoiShopV1DbContext.BatchKois.Where(bk => bk.Status == "OnSale").Include(bk => bk.BatchType).ToListAsync();
        }
        public async Task<BatchKoi> GetBatchKoi(int id)
        {
            return await _KoiShopV1DbContext.BatchKois.Where(b => b.BatchKoiId == id && b.Status == "OnSale").Include(k => k.BatchType).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BatchKoi>> GetBatchKoiWithCondition(string batchKoiName, string typeBatch, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            var allBatchKoi = _KoiShopV1DbContext.BatchKois.Where(bk => bk.Status == "OnSale").Include(bk => bk.BatchType).AsQueryable();
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
                    case "name_desc": allBatchKoi =allBatchKoi.OrderByDescending(bk => bk.Name); break;
                    case "price_asc": allBatchKoi  =allBatchKoi.OrderBy(bk => bk.Price); break;
                    case "price_desc": allBatchKoi =allBatchKoi.OrderByDescending(bk => bk.Price); break;
                    default: allBatchKoi =allBatchKoi.OrderBy(bk => bk.Name); break;
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
        public async Task<BatchKoi> GetBatchKoiSold(int id)
        {
            return await _KoiShopV1DbContext.BatchKois.Where(b => b.BatchKoiId == id && b.Status == "Sold").Include(k => k.BatchType).FirstOrDefaultAsync();
        }

        // BatchKoi Methods ===========================================================================================
        public async Task<IEnumerable<BatchKoi>> GetBatchKois()
        {
            return await _KoiShopV1DbContext.BatchKois.ToListAsync();
        }
        public async Task<bool> AddBatchKoi(BatchKoi batchKoi)
        {
            _KoiShopV1DbContext.BatchKois.Add(batchKoi);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateBatchKoi(BatchKoi batchKoi)
        {
            _KoiShopV1DbContext.BatchKois.Update(batchKoi);
            var result = await _KoiShopV1DbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<BatchKoi> GetBatchKoiById(int id)
        {
            return await _KoiShopV1DbContext.BatchKois.FindAsync(id);
        }

        // BatchKoiCategory Methods ===========================================================================================
        public async Task<IEnumerable<BatchKoiCategory>> GetBatchKoiCategories()
        {
            return await _KoiShopV1DbContext.BatchKoiCategories.ToListAsync();
        }
        public async Task<BatchKoiCategory> GetBatchKoiCategoryById(int id)
        {
            return await _KoiShopV1DbContext.BatchKoiCategories.FindAsync(id);
        }
        public async Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategory(int batchTypeId)
        {
            return await _KoiShopV1DbContext.BatchKois.Where(batchKoi => batchKoi.BatchTypeId == batchTypeId).ToListAsync();
        }
    }
}
