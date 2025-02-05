using AutoMapper;

using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetProductCategories;
public sealed class GetProductsByCategoriesHandler : IRequestHandler<GetProductsByCategoriesQuery, List<string>>
{
    private readonly IProductRepository _repository;

    public GetProductsByCategoriesHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> Handle(GetProductsByCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetProductCategoriesAsync(cancellationToken);
    }
}
