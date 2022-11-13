using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.ApplicationCore.Services;
public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ICollection<Customer>> GetAsync()
    {
        return await _customerRepository.GetAsync();
    }

    public async Task<Customer?> GetAsync(int id)
    {
        return await _customerRepository.GetAsync(id);
    }

    public async Task<Customer> AddAsync(Customer dto)
    {
        return await _customerRepository.AddAsync(dto);
    }

    public async Task<int> UpdateAsync(Customer dto)
    {
        return await _customerRepository.UpdateAsync(dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _customerRepository.DeleteAsync(id);
    }
}
