using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using KoiShop.Application.AddressDetail.Dtos;

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

        public async Task<IEnumerable<AddressDetail>> GetAll(string userID)
        {
            var addresses = await koiShopV1DbContext.AddressDetails
                .Where(address => address.UserId == userID)
                .ToListAsync<KoiShop.Domain.Entities.AddressDetail>();

            return addresses;
        }
            
    }
}
