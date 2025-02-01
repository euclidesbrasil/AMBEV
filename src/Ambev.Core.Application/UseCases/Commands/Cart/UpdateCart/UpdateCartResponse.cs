using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Cart.UpdateCart;

public class UpdateCartResponse : CartBaseDTO
{
    public int Id { get; init; }
}
