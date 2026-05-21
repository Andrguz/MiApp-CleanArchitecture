using MediatR;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        
        if (product == null)
            throw new KeyNotFoundException($"No se encontró el producto para eliminar.");

        await _productRepository.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}