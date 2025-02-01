using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Interfaces;

namespace Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;

public sealed record GetProductsByCategoriesResponse
{
    public List<GetProductsByCategoriesDataResponse> Data { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}

public sealed record GetProductsByCategoriesDataResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public GetProductsByCategoriesResponseRatingResponse Rating { get; set; }
}

public class GetProductsByCategoriesResponseRatingResponse
{
    public double Rate { get; set; }
    public int Count { get; set; }
}