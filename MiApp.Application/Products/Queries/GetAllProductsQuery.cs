using MediatR;
using MiApp.Application.Products.DTOs;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Products.Queries;

// 1. LA CONSULTA (No necesita parámetros porque trae todos)
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductResponse>>;

// 2. EL MANEJADOR de la consulta
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    // Solo necesitamos el repositorio para leer datos
    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // Buscamos los productos en la base de datos
        var products = await _productRepository.GetAllAsync();

        // Mapeamos la lista de entidades de dominio a nuestra lista de DTOs de respuesta
        return products.Select(p => new ProductResponse(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.Stock
        ));
    }
}