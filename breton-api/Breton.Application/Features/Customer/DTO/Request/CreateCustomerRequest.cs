namespace Breton.Application.Features.Customer.DTO.Request;

public record CreateCustomerRequest(
    int CreatedBy,
    string Name,
    string CPF,
    DateTime BirthDate,
    string Phone,
    AddressRequest Address
);