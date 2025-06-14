using Microsoft.OpenApi.Models;

namespace Api;
public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

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

        app.Use(async (context, next) => {
            if (app.Environment.IsDevelopment()) {
                context.Request.Headers.Append("Access-Control-Allow-Origin", "http://127.0.0.1:8080");
                context.Request.Headers.Append("Access-Control-Allow-Origin", "http://127.0.0.1:8081");
            } /*else
                context.Request.Headers.Append("Access-Control-Allow-Origin", "");*/

            await next.Invoke();
        });

        app.UseAuthorization();

        app.UsePathBase("/api/v1");

        app.MapControllers();

        app.Run();
    }
}