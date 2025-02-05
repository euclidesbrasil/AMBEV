using AutoMapper;

using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetCustomerById;

public sealed record GetCustomerByIdRequest(int id) : IRequest<GetCustomerByIdResponse>;