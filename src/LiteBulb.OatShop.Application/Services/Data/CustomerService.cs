using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Repositories;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class CustomerService : Service<Customer, int>
{
    public CustomerService(ILogger<CustomerService> logger, IRepository<Customer, int> customerRepository)
        : base(logger, customerRepository) { }
}
