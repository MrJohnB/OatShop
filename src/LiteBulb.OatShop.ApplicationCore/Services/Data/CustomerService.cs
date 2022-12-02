using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Repositories;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.ApplicationCore.Services.Data;
public class CustomerService : IService<Customer>
{
    private readonly ILogger<CustomerService> _logger;
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(ILogger<CustomerService> logger, IRepository<Customer> customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ServiceResponse<ICollection<Customer>>> GetAsync()
    {
        var result = await _customerRepository.GetAsync();

        if (result is null)
        {
            return new ServiceResponse<ICollection<Customer>>(true,
                "Error occurred while retrieving list of Customer objects.  Result was null for some reason.");
        }

        return new ServiceResponse<ICollection<Customer>>(result);
    }

    public async Task<ServiceResponse<Customer>> GetAsync(int id)
    {
        var result = await _customerRepository.GetAsync(id);

        if (result is null)
        {
            return new ServiceResponse<Customer>(true,
                $"Error occurred while retrieving Customer object with id '{id}'.  Customer object was not found in the database.");
        }

        return new ServiceResponse<Customer>(result);
    }

    public async Task<ServiceResponse<Customer>> AddAsync(Customer customer)
    {
        var result = await _customerRepository.AddAsync(customer);

        if (result is null)
        {
            return new ServiceResponse<Customer>(true,
                "Error occurred while adding a Customer object to database.  Result returned by add process was null for some reason.");
        }

        if (result.Id < 1)
        {
            return new ServiceResponse<Customer>(true,
                $"Error occurred while adding a Customer object to database.  Result returned by add process has an id of {result.Id} which is invalid.");
        }

        return new ServiceResponse<Customer>(result);
    }

    public async Task<ServiceResponse<int>> UpdateAsync(Customer customer)
    {
        var affectedCount = await _customerRepository.UpdateAsync(customer);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Customer object in the database.  Customer object with id '{customer.Id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Customer object in the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }

    public async Task<ServiceResponse<int>> DeleteAsync(int id)
    {
        var affectedCount = await _customerRepository.DeleteAsync(id);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Customer object from the database.  Customer object with id '{id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Customer object from the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }
}
