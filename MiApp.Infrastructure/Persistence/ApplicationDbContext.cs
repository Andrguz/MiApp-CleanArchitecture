using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Definimos las tablas del sistema
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>(); // <--- AGREGADO: Tabla de Usuarios

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

        // -------------------------------------------------------------
        // AGREGADO: Configuramos la entidad User usando Fluent API
        // -------------------------------------------------------------
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            // Al igual que con los productos, el ID se maneja desde el Dominio
            entity.Property(u => u.Id)
                  .ValueGeneratedNever();

            entity.Property(u => u.FullName)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(u => u.Email)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(u => u.PasswordHash)
                  .HasMaxLength(255)
                  .IsRequired();
        });
    }
}