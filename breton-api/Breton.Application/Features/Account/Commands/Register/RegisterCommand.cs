using Breton.Application.Features.Account.DTO.Request;
using MediatR;

namespace Breton.Application.Features.Account.Commands.Register;

public record RegisterCommand(RegisterRequest RegisterRequest) : IRequest;