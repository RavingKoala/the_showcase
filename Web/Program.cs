using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Web.Data;
using System;

namespace Web {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
            connectionStringBuilder.Password = Environment.GetEnvironmentVariable("DBPassword");
            Console.WriteLine(Environment.GetEnvironmentVariable("DBPassword"));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionStringBuilder.ConnectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddHttpClient("ApiClient", httpClient => {
                httpClient.BaseAddress = new Uri("https://localhost:7267/api/v1/");
                //httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            } else {
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
                pattern: "{controller=Profile}/{action=Index}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope()) {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in new[] { "Admin", "Moderator", "User" })
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "admin@hbo-ict.nl";
                string userName = "admin";
                string password = "Poi123!)";

                var userAcc = await userManager.FindByEmailAsync(email);
                if (await userManager.FindByEmailAsync(email) == null) {
                    IdentityUser adminUser = new IdentityUser();
                    adminUser.UserName = userName;
                    adminUser.Email = email;
                    adminUser.EmailConfirmed = true;

                    await userManager.CreateAsync(adminUser, password);
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            app.Run();
        }
    }
}