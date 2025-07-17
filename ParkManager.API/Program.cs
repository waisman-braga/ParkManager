using Microsoft.EntityFrameworkCore;
using ParkManager.Domain.Interfaces;
using ParkManager.Infrastructure.Data;
using ParkManager.Infrastructure.Repositories;
using ParkManager.API.Mapping;
using ParkManager.API.Middleware;
using ParkManager.API.Services;

namespace ParkManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Entity Framework Configuration
            builder.Services.AddDbContext<ParkManagerContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper Configuration
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Dependency Injection
            builder.Services.AddScoped<ICliente, ClienteRepository>();
            builder.Services.AddScoped<IVeiculo, VeiculoRepository>();
            builder.Services.AddScoped<IMensalista, MensalistaRepository>();
            //builder.Services.AddScoped<IFaturamentoBasico, FaturamentoBasicoRepository>();
            builder.Services.AddScoped<ValidationService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Ensure database is created and migrations are applied
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ParkManagerContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}