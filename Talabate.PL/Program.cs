using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Data;
using Talabat.Repository.IdentityData;
using Talabat.Service;
using Talabate.PL.Extention;

namespace Talabate.PL
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<StoreDbContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdentityAppDbContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnetion"));
            });
            builder.Services.AddAplicationServices();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                             .AddEntityFrameworkStores<IdentityAppDbContext>();
            builder.Services.AddScoped<ITokenService, TokenService>()
                ;
            builder.Services.AddAuthentication(Option =>
            {
                Option.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Option =>
            {
                Option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWt:Key"]))
                };
               
            });
            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            #region Seeding Data
            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {

               var userManger=  services.GetRequiredService<UserManager<AppUser>>();
                await SeedUser.CreateUser(userManger);
                var dbContext = services.GetRequiredService<StoreDbContext>();
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred IN program in Seeding DATA");
            }

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();   
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}