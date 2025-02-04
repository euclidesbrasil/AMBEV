using Ambev.Core.Application.UseCases.DTOs;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Cart.CreateCart
{
    public class CreateCartRequest : CartBaseDTO, IRequest<CreateCartResponse>
    {
    }
}
