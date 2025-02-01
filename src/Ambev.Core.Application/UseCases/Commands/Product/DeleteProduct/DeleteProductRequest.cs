using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Product.DeleteProduct;

public sealed record DeleteProductRequest(int id) : IRequest<DeleteProductResponse>;
