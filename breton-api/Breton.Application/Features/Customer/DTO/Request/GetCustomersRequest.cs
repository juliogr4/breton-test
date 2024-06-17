namespace Breton.Application.Features.Customer.DTO.Request;

public record GetCustomersRequest(
    int PageSize,
    int PageNumber,
    string? CPF,
    string? Name
);