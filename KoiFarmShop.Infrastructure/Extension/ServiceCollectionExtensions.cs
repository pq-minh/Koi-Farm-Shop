using KoiFarmShop.Infrastructure.Persistence;
using KoiFarmShop.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Interfaces;

namespace KoiFarmShop.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<KoiFarmShopContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IKoiRepository, KoiRepository>();
            services.AddScoped<IBatchKoiRepository, BatchKoiRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}

