using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetProductCategories;

public sealed record GetProductsByCategoriesQuery() : IRequest<List<string>>;
