using AutoMapper;

using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetUsersById;

public sealed record GetUsersByIdRequest(int id) : IRequest<GetUsersByIdResponse>;