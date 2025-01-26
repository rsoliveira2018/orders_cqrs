using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task PostAsync(Product product);
    Task PutAsync(Product product);
    Task DeleteAsync(Guid id);
}
