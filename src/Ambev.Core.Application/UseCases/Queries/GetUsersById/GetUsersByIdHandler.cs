using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
namespace Ambev.Core.Application.UseCases.Queries.GetUsersById;
public sealed class GetUsersByIdHandler : IRequestHandler<GetUsersByIdRequest, GetUsersByIdResponse>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersByIdHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetUsersByIdResponse> Handle(GetUsersByIdRequest query, CancellationToken cancellationToken)
    {

        User user = await _repository.Get(query.id, cancellationToken);

        return _mapper.Map<GetUsersByIdResponse>(user);
    }
}
