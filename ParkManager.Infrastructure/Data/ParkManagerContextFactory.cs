using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ParkManager.Infrastructure.Data
{
    public class ParkManagerContextFactory : IDesignTimeDbContextFactory<ParkManagerContext>
    {
        public ParkManagerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ParkManager.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ParkManagerContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString);

            return new ParkManagerContext(builder.Options);
        }
    }
}
