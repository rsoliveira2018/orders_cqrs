using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IProductQueryRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task AddOrUpdateAsync(Product product);
    Task DeleteAsync(int id);
}
