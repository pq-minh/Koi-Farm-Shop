using KoiShop.Application.Extensions;
using KoiShop.Application.Interfaces;
using KoiShop.Application.Service;
using KoiShop.Application.ServiceCatching;
using KoiShop.Infrastructure.Extensions;
using KoiShop.Infrastructure.Persistence;
using KoiShop.Infrastructure.Presenters;
using KoiShop.Infrastructure.Seeder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace KoiShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Cấu hình CORS
            builder.Services.AddCors(options => 
            {
                options.AddPolicy("AllowAllOrigin",
                    policy => policy.WithOrigins("http://localhost:5173")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddPresentations(builder.Configuration);
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<MemoryCacheService>();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; // Bật nén cho HTTPS
                options.Providers.Add<BrotliCompressionProvider>(); // Thêm Brotli
                options.Providers.Add<GzipCompressionProvider>(); // Nếu muốn hỗ trợ cả Gzip
            });

            // Tuỳ chỉnh mức độ nén Brotli
            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal; // Mức nén tối ưu
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseResponseCompression();
            app.UseMiddleware<CacheInvalidMiddleware>();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
            await seeder.Seed();

            app.UseHttpsRedirection(); 
            app.UseRouting();

            app.UseCors("AllowAllOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}
