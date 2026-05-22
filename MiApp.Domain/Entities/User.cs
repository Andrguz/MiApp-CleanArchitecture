namespace MiApp.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;

    // Constructor requerido por EF Core
    private User() {}

    public User(Guid id, string email, string passwordHash, string fullName)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("El email es obligatorio.");
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("La contraseña no puede estar vacía.");

        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
    }
}