using MediatR;
using MiApp.Application.Products.DTOs;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Products.Commands;

// 1. EL COMANDO (Lo que recibe MediatR)
public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock
) : IRequest<ProductResponse>; // Indicamos que cuando termine, devolverá un ProductResponse

// 2. EL MANEJADOR / HANDLER (La lógica del caso de uso)
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Inyectamos las interfaces que definimos en el Dominio
    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Creamos la entidad de dominio usando el constructor de tu entidad Product
        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.Stock
        );

        // Agregamos el producto al repositorio
        await _productRepository.AddAsync(product);

        // Confirmamos los cambios de forma transaccional en la base de datos física
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Mapeamos el resultado al DTO de salida para proteger el dominio
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock
        );
    }
}