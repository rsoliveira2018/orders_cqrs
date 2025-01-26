using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;

namespace Application.Commands;

public class CustomerCommandHandler(ICustomerCommandRepository commandRepository, ICustomerQueryRepository queryRepository)
{
    private readonly ICustomerCommandRepository _commandRepository = commandRepository;
    private readonly ICustomerQueryRepository _queryRepository = queryRepository;

    public async Task AddAsync(Customer customer)
    {
        await _commandRepository.AddAsync(customer);

        await _queryRepository.AddOrUpdateAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _commandRepository.UpdateAsync(customer);

        await _queryRepository.AddOrUpdateAsync(customer);
    }

    public async Task DeleteAsync(int id)
    {
        await _commandRepository.DeleteAsync(id);

        await _queryRepository.DeleteAsync(id);
    }
}
