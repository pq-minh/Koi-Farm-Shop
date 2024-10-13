using FluentValidation;
using FluentValidation.AspNetCore;
using KoiShop.Application.Interfaces;
using KoiShop.Application.JwtToken;
using KoiShop.Application.Service;
using KoiShop.Application.Users;
using Microsoft.Extensions.DependencyInjection;
using static KoiShop.Application.Users.UserContext;

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
            services.AddScoped<IUserContext, UserContext>();
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly).AddFluentValidationAutoValidation();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<FirebaseService>();
            services.AddScoped<IKoiService, KoiService>();
            services.AddScoped<IBatchKoiService, BatchKoiService>();
            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
