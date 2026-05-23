using Microsoft.EntityFrameworkCore;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Infrastructure.Persistence
{
    public class ComandasDbContext : DbContext
    {
        public ComandasDbContext(DbContextOptions<ComandasDbContext> options) : base(options)
        {
        }

        // Aquí registramos tu entidad del Dominio como una tabla en la base de datos
        public DbSet<Comanda> Comandas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de Clean Architecture para la tabla
            modelBuilder.Entity<Comanda>().HasKey(c => c.Id);
            modelBuilder.Entity<Comanda>().Property(c => c.NombreCliente).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Comanda>().Property(c => c.DescripcionPedido).IsRequired().HasMaxLength(500);
        }
    }
}