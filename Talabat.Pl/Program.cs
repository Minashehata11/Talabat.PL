using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat.Pl
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<StoreDbContext>(Option=>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
           // builder.Services.AddScoped<StoreDbContext>();
            var app = builder.Build();
            #region Seeding Data
            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var LoggerFactory=services.GetRequiredService<ILoggerFactory>();

            try{

                var dbContext=services.GetRequiredService<StoreDbContext>();
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch(Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred IN program in Seeding DATA");
            }
            
            #endregion
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();  
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}