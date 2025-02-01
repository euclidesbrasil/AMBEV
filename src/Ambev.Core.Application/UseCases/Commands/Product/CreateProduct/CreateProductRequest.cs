using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Aggregate;
using MediatR;
using Ambev.Core.Domain.ValueObjects;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Product.CreateProduct
{
    public class CreateProductRequest : ProductBaseDTO, IRequest<CreateProductResponse>
    {
    }
}
