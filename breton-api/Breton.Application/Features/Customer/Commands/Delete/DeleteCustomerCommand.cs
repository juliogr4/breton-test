using MediatR;

namespace Breton.Application.Features.Customer.Commands.Delete;

public record DeleteCustomerCommand(int Id) : IRequest;