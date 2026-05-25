using MediatR;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Users.Commands;

// El comando recibe los datos que viajan desde Swagger
public record RegisterUserCommand(string Email, string Password, string FullName) : IRequest<Guid>;

// El Handler maneja la lógica de negocio usando el repositorio
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validamos si el email ya existe para que no se duplique
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        // Creamos la entidad respetando tu constructor seguro del Dominio
        var userId = Guid.NewGuid();
        var newUser = new User(
            userId,
            request.Email,
            request.Password, // Va en texto plano como manejás el login
            request.FullName
        );

        // Guardamos en el repositorio e impactamos en SQLite
        await _userRepository.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync(); 

        return userId;
    }
}