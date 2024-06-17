using System.Data;
using System.Text.Json;
using Breton.Domain.Common.Pagination;
using Breton.Domain.Entities.Customer.AggregateRoot;
using Breton.Domain.Entities.Customer.Parameters.Request;
using Breton.Domain.Entities.Customer.ValueObjects;
using Breton.Domain.Repository;
using Breton.Infrastructure.Data;
using Dapper;

namespace Breton.Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _dapperContext;
    public CustomerRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task CreateCustomer(CustomerEntity customer)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_customer_create;

            var parameters = new DynamicParameters();
            parameters.Add("@created_by", customer.CreatedBy);
            parameters.Add("@name", customer.Name);
            parameters.Add("@cpf", customer.CPF);
            parameters.Add("@birth_date", customer.BirthDate);
            parameters.Add("@phone", customer.Phone);
            parameters.Add("@postal_code", customer.Address.PostalCode);
            parameters.Add("@street", customer.Address.Street);
            parameters.Add("@complement", customer.Address.Complement);
            parameters.Add("@neighborhood", customer.Address.Neighborhood);
            parameters.Add("@city", customer.Address.City);
            parameters.Add("@state", customer.Address.State);
            parameters.Add("@ibge", customer.Address.Ibge);
            parameters.Add("@gia", customer.Address.Gia);
            parameters.Add("@ddd", customer.Address.DDD);
            parameters.Add("@siafi", customer.Address.Siafi);

            await conn.ExecuteAsync(procedure, parameters);
        }
    }

    public async Task DeleteCustomer(int id)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_customer_delete;

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            await conn.ExecuteAsync(procedure, parameters);
        }
    }

    public async Task<AddressBR?> GetAddressByPostalCode(string postalCode)
    {
        try 
        {
            var url = $"https://viacep.com.br/ws/{postalCode}/json/";
            
            using(HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    AddressBR address = JsonSerializer.Deserialize<AddressBR>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    return address;
                }

                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CustomerEntity?> GetCustomerByCpf(string cpf)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_customer_get_by_cpf;

            var parameters = new DynamicParameters();
            parameters.Add("@cpf", cpf);

            return await conn.QuerySingleOrDefaultAsync<CustomerEntity?>(procedure, parameters);
        }
    }

    public async Task<CustomerEntity?> GetCustomerById(int id)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_customer_get_by_id;

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var result = await conn.QueryAsync<CustomerEntity, AddressEN, CustomerEntity>(
                procedure,
                (customer, address) =>
                {
                    customer.Address = address;
                    return customer;
                },
                parameters,
                splitOn: "postalCode"
            );

            return result.FirstOrDefault();
        }
    }

    public async Task<PaginatedList<CustomerEntity>> GetCustomers(GetCustomersParametersRequest customers)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = "pr_customer_get";

            var parameters = new DynamicParameters();
            parameters.Add("@page_size", customers.PageSize);
            parameters.Add("@page_number", customers.PageNumber);
            parameters.Add("@cpf", customers.CPF);
            parameters.Add("@name", customers.Name);

            using (var multi = await conn.QueryMultipleAsync(procedure, parameters))
            {
                var data = multi.Read<CustomerEntity, AddressEN, CustomerEntity>(
                    (customer, address) =>
                    {
                        customer.Address = address;
                        return customer;
                    },
                    splitOn: "postalCode"
                ).ToList();

                var totalCount = multi.ReadSingle<int>();
                
                return new PaginatedList<CustomerEntity>(totalCount, data);
            }
        }
    }

    public async Task UpdateCustomer(int id, CustomerEntity customer)
    {
        using(var conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_customer_update;

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@name", customer.Name);
            parameters.Add("@cpf", customer.CPF);
            parameters.Add("@birth_date", customer.BirthDate);
            parameters.Add("@phone", customer.Phone);
            parameters.Add("@postal_code", customer.Address.PostalCode);
            parameters.Add("@street", customer.Address.Street);
            parameters.Add("@complement", customer.Address.Complement);
            parameters.Add("@neighborhood", customer.Address.Neighborhood);
            parameters.Add("@city", customer.Address.City);
            parameters.Add("@state", customer.Address.State);
            parameters.Add("@ibge", customer.Address.Ibge);
            parameters.Add("@gia", customer.Address.Gia);
            parameters.Add("@ddd", customer.Address.DDD);
            parameters.Add("@siafi", customer.Address.Siafi);

            await conn.ExecuteAsync(procedure, parameters);
        }
    }
}