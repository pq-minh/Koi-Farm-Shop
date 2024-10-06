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
    public class AddressDetailsRepository(KoiShopV1DbContext koiShopV1DbContext) : IAddressDetailRepository
    {
        public  async Task<int> Create(AddressDetail entity)
        {
            koiShopV1DbContext.AddressDetails?.AddAsync(entity);
            await koiShopV1DbContext.SaveChangesAsync();
            return entity.AddressId;
        }
    }
}
