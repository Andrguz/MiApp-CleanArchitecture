using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Auth;
using MiApp.Application.Users.Commands; // <--- AGREGADO: Para que reconozca el RegisterUserCommand

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

    // -------------------------------------------------------------
    // NUEVO ENDPOINT: POST /api/Auth/register
    // -------------------------------------------------------------
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            var userId = await _mediator.Send(command);
            return Ok(new { message = "Usuario registrado con éxito.", userId = userId });
        }
        catch (Exception ex)
        {
            // Si el email ya existe o falla la validación, devuelve un 400 Bad Request
            return BadRequest(new { message = ex.Message });
        }
    }
}