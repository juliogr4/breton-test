using Breton.Application.Features.Customer.DTO.Request;
using MediatR;

namespace Breton.Application.Features.Customer.Commands.Create;

public record CreateCustomerCommand(CreateCustomerRequest CreateCustomerRequest) : IRequest;