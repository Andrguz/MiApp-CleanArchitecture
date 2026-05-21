using FluentValidation;

namespace MiApp.Application.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // Reglas para el Nombre
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        // Reglas para la Descripción
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.")
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

        // Reglas para el Precio
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser un número mayor a cero.");

        // Reglas para el Stock
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser un número negativo.");
    }
}