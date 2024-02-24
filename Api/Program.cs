using Microsoft.OpenApi.Models;

namespace Api {
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

            app.UseAuthorization();

            app.UsePathBase("/api/v1");

            app.MapControllers();

            app.Run();
        }
    }
}