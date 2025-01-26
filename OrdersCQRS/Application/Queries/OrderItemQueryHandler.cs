using Core.Entities;
using Core.Interfaces.Repositories.Queries;

namespace Application.Queries;

public class OrderItemQueryHandler(IOrderItemQueryRepository queryRepository)
{
    private readonly IOrderItemQueryRepository _queryRepository = queryRepository;

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _queryRepository.GetAllAsync();
    }

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        return await _queryRepository.GetByIdAsync(id);
    }
}

