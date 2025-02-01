using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
namespace Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
public sealed class GetProductsByCategoriesHandler : IRequestHandler<GetProductsByCategoriesRequest, GetProductsByCategoriesResponse>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsByCategoriesHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetProductsByCategoriesResponse> Handle(GetProductsByCategoriesRequest query, CancellationToken cancellationToken)
    {
        var paginationQuery = new PaginationQuery
        {
            Page = query.page,
            Size = query.size,
            Order = query.order
        };

        PaginatedResult<Product> listProduct = await _repository.GetProductsByCategoriesAsync(query.name, paginationQuery, cancellationToken);
        var data = listProduct.Data.Select(x => new GetProductsByCategoriesDataResponse()
        {
            Category = x.Category,
            Description = x.Description,
            Image = x.Image,
            Price = x.Price,
            Title = x.Title,
            Rating = new GetProductsByCategoriesResponseRatingResponse() { Count = x.Rating.Count, Rate = x.Rating.Rate },
            Id = x.Id
        }).ToList();

        return new GetProductsByCategoriesResponse()
        {
            Data = data,
            CurrentPage = listProduct.CurrentPage,
            TotalItems = listProduct.TotalItems,
            TotalPages = listProduct.TotalPages
        };
    }
}
