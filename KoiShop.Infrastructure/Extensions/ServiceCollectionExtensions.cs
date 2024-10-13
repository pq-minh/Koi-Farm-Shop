using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using KoiShop.Infrastructure.Repositories;
using KoiShop.Infrastructure.Respositories;
using KoiShop.Infrastructure.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KoiShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Register service for infastructure
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("KoiShopDB");
            services.AddScoped<IBatchKoiRepository, BatchKoiRepository>();
            services.AddScoped<IKoiRepository, KoiRepository>();
            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAddressDetailRepository, AddressDetailsRepository>();
            services.AddScoped<IUserSeeder, UserSeeder>();
            services.AddDbContext<KoiShopV1DbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<KoiShopV1DbContext>().AddRoles<IdentityRole>();
        }
    }
}
