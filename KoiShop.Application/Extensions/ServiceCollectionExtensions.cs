using KoiShop.Application.JwtToken;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Extensions
{
   public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //Register service for JWTToken
            services.AddScoped<IJwtTokenService,JwtTokenService>();
            services.AddHttpContextAccessor();
            //Resgiter service for MediatR(CQRS design pattern)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            typeof(ServiceCollectionExtensions).Assembly));
        }
    }
}
