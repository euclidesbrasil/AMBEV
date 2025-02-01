using AutoMapper;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Cart.DeleteCart;

public sealed record DeleteCartRequest(int id) : IRequest<DeleteCartResponse>
{

}
