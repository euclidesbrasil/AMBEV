using AutoMapper;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Application.UseCases.Queries.GetUsersById;

public sealed class GetUsersByIdMapper : Profile
{
    public GetUsersByIdMapper()
    {
        CreateMap<User, GetUsersByIdResponse>();
    }
}
