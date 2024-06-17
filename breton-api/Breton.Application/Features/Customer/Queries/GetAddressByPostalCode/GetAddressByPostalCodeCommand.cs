using Breton.Application.Features.Customer.DTO.Response;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetAddressByPostalCode;

public record GetAddressByPostalCodeCommand(string PostalCode) : IRequest<AddressResponse>;