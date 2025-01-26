using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface IProductCommandRepository
{
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
