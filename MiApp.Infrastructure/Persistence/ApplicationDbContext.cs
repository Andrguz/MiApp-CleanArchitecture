using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Definimos la tabla de Productos
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuramos la entidad Product usando Fluent API
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            // Le indicamos a EF Core que el ID lo manejamos desde el Dominio (con Guid.NewGuid())
            entity.Property(p => p.Id)
                  .ValueGeneratedNever();

            entity.Property(p => p.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(p => p.Description)
                  .HasMaxLength(500);

            entity.Property(p => p.Price)
                  .HasColumnType("TEXT") // SQLite maneja los decimales como TEXT o REAL de forma segura
                  .IsRequired();

            entity.Property(p => p.Stock)
                  .IsRequired();
        });
    }
}