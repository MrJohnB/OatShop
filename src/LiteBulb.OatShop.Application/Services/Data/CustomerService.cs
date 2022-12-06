using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Repositories;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class CustomerService : Service<Customer>
{
    public CustomerService(ILogger<CustomerService> logger, IRepository<Customer> customerRepository)
        : base(logger, customerRepository) { }
}
