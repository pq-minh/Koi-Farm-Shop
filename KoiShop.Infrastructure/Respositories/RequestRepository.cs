using Azure.Core;
using KoiShop.Application.Command.CreateRequest;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Request = KoiShop.Domain.Entities.Request;

namespace KoiShop.Infrastructure.Respositories
{
    public class RequestRepository(KoiShopV1DbContext koiShopV1DbContext) : IRequestRepository
    {
        public async Task<Koi> CreateRequest(Koi entity)
        {
           await koiShopV1DbContext.Kois.AddAsync(entity);
            await koiShopV1DbContext.SaveChangesAsync();
            return entity;
        }

       public async Task<PaginatedResult<Request>> GetAllRequest(string userId, int pageNumber, int pageSize)
        {
            var query = koiShopV1DbContext.Requests
                .Where(us => us.UserId == userId && (us.TypeRequest.Equals("online") || us.TypeRequest.Equals("offline")))
                .Include(pk => pk.Package)
                    .ThenInclude(k => k.Koi)
                .Include(r => r.Quotations);
            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<Request>(items, totalItems, pageNumber, pageSize);
        }


        public async Task<string> DecisionRequest(int rqID, string decision)
        {

            var request = await koiShopV1DbContext.Requests.FindAsync(rqID);


            if (request == null)
            {
                throw new KeyNotFoundException($"Request with ID {rqID} not found.");
            }
            var quotation = await koiShopV1DbContext.Quotations.FirstOrDefaultAsync(q => q.RequestId == rqID);
            if (quotation == null)
            {
                throw new KeyNotFoundException($"Quotation with ID {quotation.QuotationId} not found.");
            }
            var package = await koiShopV1DbContext.Packages.FindAsync(request.PackageId);
            if (package == null)
            {
                throw new KeyNotFoundException($"Package with ID {request.PackageId} not found.");
            }

            var kois = await koiShopV1DbContext.Kois.FindAsync(package.KoiId);
            if (kois == null)
            {
                throw new KeyNotFoundException($"Koi with ID {package.KoiId} not found.");
            }

            if (decision == "agree")
            {
                request.Status = "Completed";
                kois.Status = "OnSale";
                kois.Price = request.AgreementPrice;
                quotation.Status = "Completed";
            }
            else if (decision == "reject")
            {
                request.Status = "Rejected";
                kois.Status = "Canceled";
                quotation.Status = "Rejected";
            }
            else
            {
                throw new ArgumentException("Invalid decision. Use 'agree' or 'reject'.", nameof(decision));
            }

            koiShopV1DbContext.Update(request);
            koiShopV1DbContext.Update(quotation);
            koiShopV1DbContext.Update(kois);

            await koiShopV1DbContext.SaveChangesAsync();


            return request.Status;
        }
    }
}
