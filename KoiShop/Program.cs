using KoiShop.Application.Extensions;
using KoiShop.Infrastructure.Extensions;
using KoiShop.Infrastructure.Presenters;
using KoiShop.Infrastructure.Seeder;

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
