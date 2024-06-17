namespace Breton.Application.Features.Account.DTO.Request;

public record AuthenticationRequest(
    string Email,
    string Password
);