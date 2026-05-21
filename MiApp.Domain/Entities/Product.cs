using MiApp.Domain.Exceptions;

namespace MiApp.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    protected Product() { }

    public Product(string name, string description, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del producto no puede estar vacío.");

        if (price < 0)
            throw new DomainException("El precio no puede ser menor a cero.");

        if (stock < 0)
            throw new DomainException("El stock inicial no puede ser negativo.");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public void UpdateStock(int amount)
    {
        if (Stock + amount < 0)
            throw new DomainException("No hay suficiente stock disponible.");

        Stock += amount;
    }

    public void UpdateDetails(string name, string description, decimal price, int stock)
    {
        if (price <= 0) throw new ArgumentException("El precio debe ser un número mayor a cero.");
        if (stock < 0) throw new ArgumentException("El stock no puede ser un número negativo.");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }
}