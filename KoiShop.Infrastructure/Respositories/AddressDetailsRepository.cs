using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using KoiShop.Application.AddressDetail.Dtos;
using System.Runtime.CompilerServices;

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
        public async Task<bool> UpdateAddress(int id,string city, string distric,string ward,string streetname)
        {
            var address = await koiShopV1DbContext.AddressDetails.FirstOrDefaultAsync(ad => ad.AddressId == id);
            if ( address == null)
            {
                return false;
            } 
            address.City = city;
            address.Dictrict= distric;
            address.Ward = ward;
            address.StreetName = streetname;
            koiShopV1DbContext.Update(address);
            await koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
            
    }
}
