using Breton.Application.Features.Customer.DTO.Response;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetCustomerById;

public record GetCustomerByIdCommand(int Id) : IRequest<CustomerResponse>;
