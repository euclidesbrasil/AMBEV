using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;

public sealed record GetProductsByCategoriesRequest(string name, int page , int size, string order, Dictionary<string, string> filters = null) : IRequest<GetProductsByCategoriesResponse>;