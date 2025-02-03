using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Aggregate;
using MediatR;
using Ambev.Core.Domain.ValueObjects;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Cart.UpdateCart
{
    public class UpdateCartRequest : CartBaseDTO, IRequest<UpdateCartResponse>
    {
        public int Id { get; internal set; }

        public void SetIdContext(int id)
        {
            Id = id;
        }

        public int GetIdContext()
        {
            return Id;
        }
    }
}
