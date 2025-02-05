using Ambev.Core.Domain.Entities;

using MediatR;
using Ambev.Core.Domain.ValueObjects;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Product.UpdateProduct
{
    public class  UpdateProductRequest: ProductDTO, IRequest<UpdateProductResponse>
    {
       public int Id { get; internal set; }
       public void SetId(int id)
       {
           Id = id;
       }
    }
}
