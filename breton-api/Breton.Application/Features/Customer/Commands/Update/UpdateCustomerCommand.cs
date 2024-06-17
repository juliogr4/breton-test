using Breton.Application.Features.Customer.DTO.Request;
using MediatR;

namespace Breton.Application.Features.Customer.Commands.Update;

public record UpdateCustomerCommand(int Id, UpdateCustomerRequest UpdateCustomerRequest) : IRequest;