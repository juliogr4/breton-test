using Breton.Domain.Entities.Account;
using Breton.Domain.Entities.Customer.ValueObjects;

namespace Breton.Domain.Entities.Customer.AggregateRoot;

public class CustomerEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public string Name { get; set; } = String.Empty;
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; }
    public AddressEN Address { get; set; }
    public bool IsActive { get; set; }
}