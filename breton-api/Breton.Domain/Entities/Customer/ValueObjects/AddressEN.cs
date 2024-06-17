namespace Breton.Domain.Entities.Customer.ValueObjects;

public struct AddressEN
{
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Ibge { get; set; }
    public string Gia { get; set; }
    public string DDD { get; set; }
    public string Siafi { get; set; }
}