using Breton.Domain.Common.Pagination;
using Breton.Domain.Entities.Customer.AggregateRoot;
using Breton.Domain.Entities.Customer.Parameters.Request;
using Breton.Domain.Entities.Customer.ValueObjects;

namespace Breton.Domain.Repository;

public interface ICustomerRepository
{
    Task CreateCustomer(CustomerEntity customer);
    Task DeleteCustomer(int id);
    Task UpdateCustomer(int id, CustomerEntity customer);
    Task<CustomerEntity?> GetCustomerById(int id);
    Task<PaginatedList<CustomerEntity>> GetCustomers(GetCustomersParametersRequest customers);
    Task<AddressBR?> GetAddressByPostalCode(string postalCode);
    Task<CustomerEntity?> GetCustomerByCpf(string cpf);
}