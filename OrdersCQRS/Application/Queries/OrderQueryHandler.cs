using Core.Entities;
using Core.Interfaces.Repositories.Queries;

namespace Application.Queries;

public class OrderQueryHandler(IOrderQueryRepository queryRepository)
{
    private readonly IOrderQueryRepository _queryRepository = queryRepository;

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _queryRepository.GetAllAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _queryRepository.GetByIdAsync(id);
    }
}

