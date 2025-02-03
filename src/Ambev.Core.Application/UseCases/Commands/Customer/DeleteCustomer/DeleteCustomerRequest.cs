using AutoMapper;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Application.UseCases.Commands.Cart.DeleteCart;

namespace Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;

public sealed record DeleteCustomerRequest(int id) : IRequest<DeleteCustomerResponse>;