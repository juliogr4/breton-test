using Breton.Application.Features.Account.DTO.Request;
using MediatR;

namespace Breton.Application.Features.Account.Queries.Authentication;

public record AuthenticationQuery(AuthenticationRequest AuthenticationRequest) : IRequest<string>;