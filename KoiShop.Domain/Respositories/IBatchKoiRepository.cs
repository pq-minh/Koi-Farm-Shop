﻿using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IBatchKoiRepository
    {
        Task<IEnumerable<BatchKoi>> GetBatchKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize);
        /*
        Task<IEnumerable<BatchKoi>> GetAllBatch();
        Task<BatchKoi> GetBatchKoi(int id);
        Task<BatchKoi> GetBatchKoiSold(int id);
        // BatchKoi Method ============================================================================================
        Task<IEnumerable<BatchKoi>> GetBatchKois();
        Task<bool> AddBatchKoi(BatchKoi batchKoi);
        Task<bool> UpdateBatchKoi(BatchKoi batchKoi);
        Task<BatchKoi> GetBatchKoiById(int id);
        */
    }
}
