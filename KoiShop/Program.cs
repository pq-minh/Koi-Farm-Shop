using KoiShop.Application.Extensions;
using KoiShop.Infrastructure.Extensions;
using KoiShop.Infrastructure.Presenters;
using KoiShop.Infrastructure.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KoiShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cấu hình các dịch vụ
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Cấu hình CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    policy => policy.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddPresentations(builder.Configuration);

            var app = builder.Build();

            // Nếu đang ở môi trường Development, bật Swagger UI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Tạo scope và gọi phương thức Seed để khởi tạo dữ liệu
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
            await seeder.Seed();

            // Thiết lập middleware
            app.UseHttpsRedirection(); // Chuyển hướng sang HTTPS nếu cần thiết
            app.UseRouting();

            // Áp dụng CORS trước khi Authentication và Authorization
            app.UseCors("AllowAllOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            // Map các controller
            app.MapControllers();

            app.Run();
        }
    }

}
