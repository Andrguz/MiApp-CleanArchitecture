using MediatR;
using MiApp.Application.Products.DTOs;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, int Stock) : IRequest<ProductResponse>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        
        if (product == null)
            throw new KeyNotFoundException($"No se encontró el producto para actualizar.");

        // Modificamos usando el método de tu entidad de dominio (que tiene las validaciones internas)
        product.UpdateDetails(request.Name, request.Description, request.Price, request.Stock);

        await _productRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponse(product.Id, product.Name, product.Description, product.Price, product.Stock);
    }
}