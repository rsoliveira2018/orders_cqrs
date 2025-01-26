using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;

namespace Application.Commands;

public class OrderCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
{
    private readonly IOrderCommandRepository _commandRepository = commandRepository;
    private readonly IOrderQueryRepository _queryRepository = queryRepository;

    public async Task AddAsync(Order order)
    {
        await _commandRepository.AddAsync(order);

        await _queryRepository.AddOrUpdateAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        await _commandRepository.UpdateAsync(order);

        await _queryRepository.AddOrUpdateAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        await _commandRepository.DeleteAsync(id);

        await _queryRepository.DeleteAsync(id);
    }
}
