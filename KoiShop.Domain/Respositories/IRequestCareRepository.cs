﻿using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IRequestCareRepository
    {
        Task<IEnumerable<Request>> GetKoiOrBatchCare();
        Task<IEnumerable<OrderDetail>> GetCurrentOrderdetail();
        Task<bool> AddKoiOrBatchToPackage(List<OrderDetail> orderDetails);
        Task<bool> AddKoiOrBatchToRequest(List<OrderDetail> orderDetails, DateTime endDate);
        Task<bool> UpdateKoiOrBatchToCare(int? id);
    }
}
