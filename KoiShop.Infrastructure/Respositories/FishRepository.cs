using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using KoiShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EllipticCurve.Utils;
using System.Linq;

namespace KoiShop.Infrastructure.Respositories
{
    internal class FishRepository : IFishRepository
    {
        private readonly IDbContextFactory<KoiShopV1DbContext> _dbContextFactory;
        
        public FishRepository(IDbContextFactory<KoiShopV1DbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<T>> GetAllFish<T>() where T : class
        {
            using (var dbcontext = _dbContextFactory.CreateDbContext())
            {
                return await dbcontext.Set<T>().ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetFishFormType<T>(string type) where T : class
        {
            string? fishStatus = GetStatus(type);
            if (fishStatus == null)
                return Enumerable.Empty<T>();

            using (var context = _dbContextFactory.CreateDbContext())
            {

                if (fishStatus != Variables.STATUS_FISH_ALL)
                   return await context.Set<T>().Where(f => EF.Property<string>(f, "Status") == fishStatus).Include(k => EF.Property<Object>(k, "FishType")).ToListAsync();
                else
                    return await context.Set<T>().Include(k => EF.Property<Object>(k, "FishType")).ToListAsync();
            }
        }

        public async Task<T?> GetFishByIdFromType<T>(int id, string type) where T : class
        {
            string fishIdStatus;
            string? fishStatus = GetStatus(type);

            if (typeof(T) == typeof(Koi))
            {
                fishIdStatus = "KoiId";
            }
            else if (typeof(T) == typeof(BatchKoi))
            {
                fishIdStatus = "BatchKoiId";
            }
            else
                return null;

            if (fishStatus == null)
                return null;

            using (var context = _dbContextFactory.CreateDbContext())
            {
                var query = context.Set<T>().Where(k => EF.Property<int>(k, fishIdStatus) == id);

                if (GetStatus(type) != Variables.STATUS_FISH_ALL)
                    query = query.Where(k => EF.Property<string>(k, "Status") == fishStatus);

                return await query.Include(k => EF.Property<object>(k,"FishType")).FirstOrDefaultAsync();
            }
        }

        private string? GetStatus(string type)
        {
            if (type == Variables.STATUS_FISH_ONSALE)
            {
                return Variables.STATUS_FISH_ONSALE;
            }
            else if (type == Variables.STATUS_FISH_SOLD)
            {
                return Variables.STATUS_FISH_SOLD;
            }
            else if (type == Variables.STATUS_FISH_ALL)
            {
                return Variables.STATUS_FISH_ALL;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Koi>> GetKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var allKoi = context.Kois.Where(k => k.Status == Variables.STATUS_FISH_ONSALE).Include(k => k.FishType).AsQueryable();
                #region Filtering
                if (!string.IsNullOrEmpty(koiName))
                {
                    allKoi = allKoi.Where(k => k.Name.ToLower().Contains(koiName.ToLower()));
                }
                if (!string.IsNullOrEmpty(typeFish))
                {
                    allKoi = allKoi.Where(k => k.FishType.TypeFish.ToLower().Contains(typeFish.ToLower()));
                }
                if (from.HasValue)
                {
                    allKoi = allKoi.Where(k => k.Price >= from);
                }
                if (to.HasValue)
                {
                    allKoi = allKoi.Where(k => k.Price <= to);
                }
                #endregion

                #region Sorting
                //Default sort asc by name
                allKoi = allKoi.OrderBy(k => k.Name);
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy)
                    {
                        case "name_desc": allKoi = allKoi.OrderByDescending(k => k.Name); break;
                        case "price_asc": allKoi = allKoi.OrderBy(k => k.Price); break;
                        case "price_desc": allKoi = allKoi.OrderByDescending(k => k.Price); break;
                        default: allKoi = allKoi.OrderBy(k => k.Name); break;
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
                allKoi = allKoi.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                #endregion

                return await allKoi.ToListAsync();
            }
        }

        // Koi Methods ============================================================================================

        public async Task<bool> AddFish<T>(T fish) where T : class 
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Add(fish);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<bool> UpdateFish<T>(T fish) where T : class
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Update(fish);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
        }

        // FishCategory Methods =====================================================================================
        public async Task<IEnumerable<FishCategory>> GetAllFishCategories()
        {
            using (var context = _dbContextFactory.CreateDbContext())
                return await context.FishCategories.ToListAsync();
        }
        public async Task<FishCategory?> GetFishCategoryById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
                return await context.FishCategories.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetFishCategory<T>(int fishTypeId) where T : class
        {
            using (var context = _dbContextFactory.CreateDbContext())
                return await context.Set<T>().Where(k => EF.Property<int>(k,"FishTypeId") == fishTypeId).ToListAsync();
        }

    }
}
