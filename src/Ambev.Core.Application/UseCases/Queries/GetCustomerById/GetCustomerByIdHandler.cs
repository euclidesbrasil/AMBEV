using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
namespace Ambev.Core.Application.UseCases.Queries.GetCustomerById;
public sealed class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest query, CancellationToken cancellationToken)
    {
        Customer customer = await _repository.Get(query.id, cancellationToken);
        return _mapper.Map<GetCustomerByIdResponse>(customer);
    }
}
