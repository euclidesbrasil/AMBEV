using AutoMapper;

using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using MediatR;
using Ambev.Core.Domain.Enum;
using System.Threading;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public class CreateCustomerHandler :
       IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;

    public CreateCustomerHandler(IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        IMapper mapper,
        IJwtTokenService tokenService
        )
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {

        var customer = _mapper.Map<Ambev.Core.Domain.Entities.Customer>(request);
        _customerRepository.Create(customer);
        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<CreateCustomerResponse>(customer);
    }
}
