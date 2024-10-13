using KoiFarmShop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using KoiFarmShop.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KoiFarmShop.Application.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddScoped<FirebaseService>();
            services.AddScoped<IKoiService, KoiService>();
            services.AddScoped<IBatchKoiService, BatchKoiService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
