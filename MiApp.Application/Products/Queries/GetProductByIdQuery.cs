using MediatR;
using MiApp.Application.Products.DTOs;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        
        if (product == null)
            throw new KeyNotFoundException($"No se encontró el producto con el ID: {request.Id}");

        return new ProductResponse(product.Id, product.Name, product.Description, product.Price, product.Stock);
    }
}