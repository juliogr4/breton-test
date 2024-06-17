namespace Breton.Application.Features.Account.DTO.Request;

public record RegisterRequest(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword
);