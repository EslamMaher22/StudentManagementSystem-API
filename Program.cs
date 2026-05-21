
using Day2.Mapconfig;
using Day2.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<Models.ITIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("iticon")));

            builder.Services.AddScoped<UnitOfWork>();


            // auto mapper
            builder.Services.AddAutoMapper(op => op.AddProfile<MappingConfig>());

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(txt);
            app.MapControllers();

            app.Run();
        }
    }
}
