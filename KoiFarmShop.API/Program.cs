using KoiFarmShop.Application.Services;
using KoiFarmShop.Domain.Interfaces;
using KoiFarmShop.Infrastructure.Persistence;
using KoiFarmShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Infrastructure.Extension;
using KoiFarmShop.Application.Extension;

namespace KoiFarmShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<KoiFarmShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


            // Dependency Injection for Repositories and Services
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();


            // Thêm dịch vụ logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole(); // Ghi log ra console
            builder.Logging.AddDebug();   // Ghi log ra debug


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
