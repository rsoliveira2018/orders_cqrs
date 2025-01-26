using Core.Entities;
using Core.Interfaces.Repositories.Queries;

namespace Application.Queries;

public class CustomerQueryHandler(ICustomerQueryRepository queryRepository)
{
    private readonly ICustomerQueryRepository _queryRepository = queryRepository;

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _queryRepository.GetAllAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _queryRepository.GetByIdAsync(id);
    }
}

