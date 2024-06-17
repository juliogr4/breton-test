using Breton.Application.Features.Account.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Breton.Application.Features.Account.Commands.Register;
using Breton.Application.Features.Account.Queries.Authentication;
using Breton.Application.Features.Account.Commands;

namespace Breton.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        await _mediator.Send(new RegisterCommand(registerRequest));
        return Ok();
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationRequest)
    {
      string token = await _mediator.Send(new AuthenticationQuery(authenticationRequest));
      return Ok(token);
    }

    [HttpGet("confirm-email/{emailToken}")]
    public async Task<IActionResult> Authenticate(string emailToken)
    {
      await _mediator.Send(new ConfirmEmailCommand(emailToken));
      return Ok("Email confirmed successfully");
    }
}