using JokeApp.Interfaces;
using JokeApp.Models;
using JokeApp.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

namespace JokeApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        var context = new DbContextOptionsBuilder();
        context.EnableSensitiveDataLogging();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IJokeRepository, JokeRepository>();
        builder.Services.AddDbContext<JokesContext>(options =>
                                                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStringDB")));
        builder.Services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.IdleTimeout = TimeSpan.FromDays(1);
            options.Cookie.IsEssential = true;
        });
        var app = builder.Build();
        app.UseSession();
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always, // Đảm bảo rằng cookie chỉ được gửi qua HTTPS
            OnAppendCookie = cookieContext =>
            {
                cookieContext.CookieOptions.Expires = DateTimeOffset.Now.AddDays(1); // Thiết lập thời gian sống là 1 ngày
            }
        });
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Joke}/{action=Index}/{id?}");

        app.Run();
    }
}
