using KTMS.Application;
using KTMS.Application.Abstractions;
using KTMS.Infrastructure.Common;
using KTMS.Infrastructure.Database;
using KTMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KTMS.API
{
    public partial class Program
    {
        
        public static void Main(string[] args)
        {
           var builder = WebApplication.CreateBuilder(args);

            //Register DbContext
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("KTMS.Infrastructure"))
            );

            //IAppDbContext so DI can resolve it in handlers
            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetService<DatabaseContext>());

            //Register JWT Service for IJwtService  
            builder.Services.AddScoped<IJwtService, JwtService>();

            // CORS policy to allow Angular dev server access
            builder.Services.AddCors(options => 
            { 
                options.AddPolicy("AllowAngularDev", 
                    policy => 
                    { policy 
                        .WithOrigins("http://localhost:4200", "http://localhost:5500") 
                        .AllowAnyHeader() 
                        .AllowAnyMethod() 
                        .AllowCredentials(); 
                    }); 
            });

            var app = builder.Build();

            // UseCors obavezno prije UseAuthorization i UseAuthentification
            app.UseCors("AllowAngularDev");

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Infrastructure Services
            builder.Services.AddInfrastructure(builder.Configuration);

            //Add Application
            builder.Services.AddApplication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
