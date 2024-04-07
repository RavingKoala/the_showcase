using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Services;
using Web.Services.Middleware;

namespace Web;
public class Program {
    public static async Task Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
        connectionStringBuilder.Password = Environment.GetEnvironmentVariable("DBPassword");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionStringBuilder.ConnectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddHttpClient("ApiClient", httpClient => {
            httpClient.BaseAddress = new Uri("https://localhost:5000/api/v1/");
            //httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<EmailSenderOptions>(builder.Configuration);

        builder.Services.AddSingleton<ServerSentEventsService, ServerSentEventsService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseMigrationsEndPoint();
        } else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseServerSentEvents();

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