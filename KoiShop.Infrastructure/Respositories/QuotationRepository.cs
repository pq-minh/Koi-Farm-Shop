using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;


namespace KoiShop.Infrastructure.Respositories
{
    public class QuotationRepository(KoiShopV1DbContext koiShopV1DbContext) : IQuotationRepository
    {
        public async Task<Quotation> UpdatePriceQuotation(Quotation entity,string decision)
        {
            var quotation = await koiShopV1DbContext.Quotations.FindAsync(entity.QuotationId);
            var request = await koiShopV1DbContext.Requests.FindAsync(entity.RequestId);
            if (request == null || quotation == null)      
            {
                return null;
            }
            if ( decision == "agree")
            {
                quotation.Price = entity.Price;
                quotation.Note = entity.Note;
                quotation.Status = "Confirmed";
                request.AgreementPrice = entity.Price;  
                request.Status = "Confirmed";
            } else if (decision == "reject")
            {
                quotation.Status = "Rejected";
                request.Status = "Rejected";
                quotation.Note = entity.Note;
            }
           
            koiShopV1DbContext.Update(quotation);
            koiShopV1DbContext.Update(request);
            await koiShopV1DbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<PaginatedResult<QuotationWithKoi>> GetQuotation(string userid , int pageNumber, int pageSize)
        {
            var quotationsQuery = koiShopV1DbContext.Kois
         .Include(ft => ft.FishType)
         .Include(k => k.Packages)
             .ThenInclude(pk => pk.Requests)
                 .ThenInclude(rq => rq.Quotations)
         .SelectMany(k => k.Packages
             .SelectMany(p => p.Requests
                 .SelectMany(r => r.Quotations
                     .Where(q => q.UserId == userid || q.UserId == null)
                     .Select(q => new QuotationWithKoi
                     {
                         QuotationId = q.QuotationId,
                         RequestId = q.RequestId,
                         CreateDate = q.CreateDate,
                         Price = q.Price,
                         Status = q.Status,
                         UserId = q.UserId,
                         Note = q.Note,
                         KoiName = k.Name,
                         KoiImage = k.Image,
                         KoiAge = k.Age,
                         KoiWeight = k.Weight,
                         KoiSize = k.Size,
                         FishType = k.FishType
                     }))));
            var totalItems = await quotationsQuery.CountAsync();


            var items = await quotationsQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return new PaginatedResult<QuotationWithKoi>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Quotation>> GetQuotations(string status, DateTime startDate, DateTime endDate)
        {
            var quotations = await koiShopV1DbContext.Quotations
                .Where(q => q.CreateDate.HasValue &&
                             q.CreateDate >= startDate &&  
                             q.CreateDate <= endDate &&    
                             q.Status == status)
                .ToListAsync();
            return quotations;
        }



        public async Task<List<Package>> GetPackages(string status, DateTime startDate, DateTime endDate)
        {
            var packages = await koiShopV1DbContext.Quotations
                .Where(q => q.CreateDate.HasValue &&
                             q.CreateDate >= startDate &&
                             q.CreateDate <= endDate &&
                             q.Status == status)
                .Select(q => q.Request.Package)  
                .Where(p => p != null)  
                .Distinct()
                .ToListAsync();

            return packages;
        }

    }
}
