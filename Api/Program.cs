using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Api.Data;
using Microsoft.AspNetCore.Identity;

namespace Api;

public class Program {
    public static async void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        // Add services to the container.
        var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
        connectionStringBuilder.Password = Environment.GetEnvironmentVariable("DBPassword");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionStringBuilder.ConnectionString));

        builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger(c => c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers.Add(new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/api/v1" })));
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        if (!app.Environment.IsDevelopment()) {
            app.UseHsts();
        }
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UsePathBase("/api/v1");

        app.MapGroup("/Account").MapIdentityApi<IdentityUser>();

        app.MapControllers();

        
        using (var scope = app.Services.CreateScope()) {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in new[] { "Admin", "Moderator", "User" })
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // admin account
            string email = "admin@hbo-ict.nl";
            string userName = "admin";
            string password = "Poi123!";

            var userAcc = await userManager.FindByEmailAsync(email);
            if (userAcc == null) {
                IdentityUser adminUser = new IdentityUser();
                adminUser.UserName = userName;
                adminUser.Email = email;
                adminUser.EmailConfirmed = true;

                await userManager.CreateAsync(adminUser, password);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // test User
            email = "test@mail.nl";
            userName = "commonUser";
            password = "Cool999?";

            userAcc = await userManager.FindByEmailAsync(email);
            if (userAcc == null) {
                IdentityUser BasicUser = new IdentityUser();
                BasicUser.UserName = userName;
                BasicUser.Email = email;
                BasicUser.EmailConfirmed = true;

                await userManager.CreateAsync(BasicUser, password);
                await userManager.AddToRoleAsync(BasicUser, "User");
            }
        }

        app.Run();
    }
}