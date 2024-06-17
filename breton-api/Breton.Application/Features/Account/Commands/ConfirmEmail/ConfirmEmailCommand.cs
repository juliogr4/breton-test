using MediatR;

namespace Breton.Application.Features.Account.Commands;

public record ConfirmEmailCommand(string emailToken) : IRequest;