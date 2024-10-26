﻿using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IQuotationRepository
    {
        Task<Quotation> UpdatePriceQuotation(Quotation quotation, string decision);

        Task<PaginatedResult<QuotationWithKoi>> GetQuotation(string userId, int pageNumber, int pageSize);
        Task<IEnumerable<Quotation>> GetQuotations(string status, DateTime startDate, DateTime endDate);
        Task<List<Package>> GetPackages(string status, DateTime startDate, DateTime endDate);
    }
}
