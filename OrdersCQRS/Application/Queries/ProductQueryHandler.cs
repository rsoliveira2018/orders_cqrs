using Core.Entities;
using Core.Interfaces.Repositories.Queries;

namespace Application.Queries;

public class ProductQueryHandler(IProductQueryRepository queryRepository)
{
    private readonly IProductQueryRepository _queryRepository = queryRepository;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _queryRepository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _queryRepository.GetByIdAsync(id);
    }
}

