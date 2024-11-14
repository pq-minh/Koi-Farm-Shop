using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using KoiShop.Domain.Constant;

namespace KoiShop.Infrastructure.Seeder
{
    public class UserSeeder(KoiShopV1DbContext koiShopV1DbContext) : IUserSeeder
    {
        public async Task Seed()
        {
            if (await koiShopV1DbContext.Database.CanConnectAsync())
            {
                if (!koiShopV1DbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    koiShopV1DbContext.Roles.AddRange(roles);
                    await koiShopV1DbContext.SaveChangesAsync();
                }
            }
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Name = UserRoles.Customer,
            NormalizedName = UserRoles.Customer.ToUpper() 
        },
        new IdentityRole
        {
            Name = UserRoles.Admin,
            NormalizedName = UserRoles.Admin.ToUpper() 
        },
        new IdentityRole
        {
            Name = UserRoles.Staff,
            NormalizedName = UserRoles.Staff.ToUpper() 
        }
    };
            return roles;
        }
    }
}
