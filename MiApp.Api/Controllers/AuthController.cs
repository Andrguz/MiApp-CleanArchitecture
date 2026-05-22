using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Auth;

namespace MiApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(new { token = result });
        }
        catch (UnauthorizedAccessException ex)
        {
            // Si las credenciales están mal, devolvemos un 401 (No autorizado)
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}