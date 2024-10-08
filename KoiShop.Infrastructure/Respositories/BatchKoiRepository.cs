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
            return await _context.BatchKois.ToListAsync();
        }
    }
}
