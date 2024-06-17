
namespace Breton.Application.Features.Customer.DTO.Request;

public record AddressRequest(
    string PostalCode,
    string Street,
    string Complement,
    string Neighborhood,
    string City,
    string State,
    string Ibge,
    string Gia,
    string DDD,
    string Siafi
);