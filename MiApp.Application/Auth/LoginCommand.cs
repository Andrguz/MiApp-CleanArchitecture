using MediatR;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Auth;

// El comando recibe Email y Password, y devuelve un string
public record LoginCommand(string Email, string Password) : IRequest<string>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscamos si el usuario existe por Email
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Credenciales incorrectas.");
        }

        // 2. Verificamos la contraseña en texto plano (luego se puede hashear)
        if (user.PasswordHash != request.Password)
        {
            throw new UnauthorizedAccessException("Credenciales incorrectas.");
        }

        // 3. Respuesta de éxito provisoria
        return $"¡Login exitoso para {user.FullName}! (Acá va a ir el token JWT)";
    }
}