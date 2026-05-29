using Microsoft.EntityFrameworkCore;
using TShirt.Comandas.Domain.Entities;

namespace TShirt.Comandas.Infrastructure.Persistence;

public class ComandasDbContext : DbContext
{
    public ComandasDbContext(DbContextOptions<ComandasDbContext> options) : base(options) { }

    public DbSet<Comanda> Comandas => Set<Comanda>();
    public DbSet<ItemComanda> ItemsComanda => Set<ItemComanda>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Configuración de la Comanda
        modelBuilder.Entity<Comanda>(entity =>
        {
            entity.HasKey(c => c.Id);

            // Protegemos el encapsulamiento: EF Core escribirá directo en la lista privada '_items'
            entity.Metadata.FindNavigation(nameof(Comanda.Items))!
                  .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Relación 1 a Muchos
            entity.HasMany(c => c.Items)
                  .WithOne()
                  .HasForeignKey("ComandaId") // Crea la columna en la BD sin ensuciar tu Dominio
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // 2. Configuración del ItemComanda (El comodín SaaS)
        modelBuilder.Entity<ItemComanda>(entity =>
        {
            entity.HasKey(i => i.Id);

            // Esto permite guardar propiedades dinámicas sin romper la tabla relacional
            entity.Property(i => i.NotasConfiguracion)
                  .HasColumnType("jsonb");
        });
    }
}