using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories;
using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services;
using PRN222.Lab2.Services.Interfaces;

namespace PRN222.Lab2.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MyStoreContext>(options =>
                options.UseSqlServer(connectionString));


            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            builder.Services.AddScoped<IGenericRepository<AccountMember>, GenericRepository<AccountMember>>();
            builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAccountService, AccountService>();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
            {
                options.LoginPath = "/Login"; 
                options.AccessDeniedPath = "/AccessDenied"; 
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("1", policy => policy.RequireRole("1"));
                options.AddPolicy("2", policy => policy.RequireRole("2"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseSession();
            app.UseAuthentication();
            app.MapHub<SignalRServer>("/signalRServer");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
