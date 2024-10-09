using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    public class QuotationRepository(KoiShopV1DbContext koiShopV1DbContext) : IQuotationRepository
    {
        public async Task<Quotation> UpdatePriceQuotation(Quotation entity)
        {
            var quotation = await koiShopV1DbContext.Quotations.FindAsync(entity.QuotationId);
            var request = await koiShopV1DbContext.Requests.FindAsync(entity.RequestId);
            if (request == null || quotation == null)      
            {
                return null;
            }
            quotation.Price = entity.Price;
            quotation.Status = entity.Status;
            request.AgreementPrice = entity.Price;
            request.Status = entity.Status;
            koiShopV1DbContext.Update(quotation);
            koiShopV1DbContext.Update(request);
            await koiShopV1DbContext.SaveChangesAsync();
            return entity;
        }
    }
}
