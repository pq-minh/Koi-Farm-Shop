﻿using KoiShop.Domain.Entities;
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
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IQuotationRepository, QuotationRepository>();    
            services.AddScoped<IUserSeeder, UserSeeder>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRequestCareRepository, RequestCareRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<KoiShopV1DbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = true; // Cấu hình xác thực tài khoản
            })
               .AddEntityFrameworkStores<KoiShopV1DbContext>().AddRoles<IdentityRole>().AddDefaultTokenProviders(); ;
        }
    }
}
