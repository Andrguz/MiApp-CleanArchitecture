using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product); // <--- Agregado
    Task DeleteAsync(Product product); // <--- Agregado
}