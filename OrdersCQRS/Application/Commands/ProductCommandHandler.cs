using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;

namespace Application.Commands;

public class ProductCommandHandler(IProductCommandRepository commandRepository, IProductQueryRepository queryRepository)
{
    private readonly IProductCommandRepository _commandRepository = commandRepository;
    private readonly IProductQueryRepository _queryRepository = queryRepository;

    public async Task AddAsync(Product product)
    {
        await _commandRepository.AddAsync(product);

        await _queryRepository.AddOrUpdateAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _commandRepository.UpdateAsync(product);

        await _queryRepository.AddOrUpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _commandRepository.DeleteAsync(id);

        await _queryRepository.DeleteAsync(id);
    }
}
