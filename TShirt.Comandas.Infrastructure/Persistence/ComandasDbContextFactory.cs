using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TShirt.Comandas.Infrastructure.Persistence
{
    public class ComandasDbContextFactory : IDesignTimeDbContextFactory<ComandasDbContext>
    {
        public ComandasDbContext CreateDbContext(string[] args)
        {
            // Busca el archivo appsettings.json en el proyecto API
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TShirt.Comandas.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ComandasDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new ComandasDbContext(optionsBuilder.Options);
        }
    }
}