namespace Breton.Application.Features.Customer.DTO.Request;

public record UpdateCustomerRequest(
    int id,
    string Name,
    string CPF,
    DateTime BirthDate,
    string Phone,
    AddressRequest Address
);