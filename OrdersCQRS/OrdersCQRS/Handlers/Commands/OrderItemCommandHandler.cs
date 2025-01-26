using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;

namespace OrdersCQRS.Handlers.Commands;

public class OrderItemCommandHandler(IOrderItemCommandRepository commandRepository, IOrderItemQueryRepository queryRepository)
{
    private readonly IOrderItemCommandRepository _commandRepository = commandRepository;
    private readonly IOrderItemQueryRepository _queryRepository = queryRepository;

    public async Task AddAsync(OrderItem orderItem)
    {
        await _commandRepository.AddAsync(orderItem);

        await _queryRepository.AddOrUpdateAsync(orderItem);
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        await _commandRepository.UpdateAsync(orderItem);

        await _queryRepository.AddOrUpdateAsync(orderItem);
    }

    public async Task DeleteAsync(int id)
    {
        await _commandRepository.DeleteAsync(id);

        await _queryRepository.DeleteAsync(id);
    }
}
