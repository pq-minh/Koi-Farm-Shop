using KoiShop.Application.Extensions;
using KoiShop.Infrastructure.Extensions;
using KoiShop.Infrastructure.Persistence;
using KoiShop.Infrastructure.Presenters;
using KoiShop.Infrastructure.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KoiShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin", builder =>
            //    {
            //        builder.WithOrigins("http://localhost:5173")
            //               .AllowAnyHeader()
            //               .AllowAnyMethod();
            //    });
            //});

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();
            //jwt authentication
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //    };
            //});
            //authorization role
            //builder.Services.AddAuthorization(Options =>
            //{
            //    Options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            //});
            builder.Services.AddDbContext<KoiShopV1DbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddPresentations(builder.Configuration);

            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });


            // logging service
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole(); 
            builder.Logging.AddDebug();



            builder.Services.AddDbContext<KoiShopV1DbContext>(options =>
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
            //cors
            app.UseRouting();
            //app.UseCors("AllowSpecificOrigin");
            app.UseCors("AllowAll");
            //author
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            

            app.Run();
        }
    }
}
